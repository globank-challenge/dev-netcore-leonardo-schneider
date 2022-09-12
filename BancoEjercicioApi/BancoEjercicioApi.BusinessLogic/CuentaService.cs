using BancoEjercicioApi.Abstractions;
using BancoEjercicioApi.DataAccess.UnitOfWork;
using BancoEjercicioApi.Entities;
using BancoEjercicioApi.Entities.DTOs;
using BancoEjercicioApi.Entities.Mappers;
using BancoEjercicioApi.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoEjercicioApi.Services
{
    public interface ICuentaService : ICRUD<CuentaDTO>
    { 
        IList<CuentaDTO> GetByClientId(int clientId);
    }

    public class CuentaService : ICuentaService
    {
        #region Vars

        private readonly IUnitOfWork _unitOfWork;

        #endregion Vars

        #region Constructor

        public CuentaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        #region Public Methods

        public void DeleteById(int id)
        {
            Cuenta? cuenta = _unitOfWork.CuentaRepository.Find(c => c.Id == id).FirstOrDefault();
            if (cuenta != null)
            {
                // Elimina todos los movimientos
                IList<Movimiento> movimientosVinculados = _unitOfWork.MovimientoRepository.Find(c => c.CuentaId == id).ToList();
                foreach (Movimiento movimiento in movimientosVinculados)
                {
                    _unitOfWork.MovimientoRepository.DeleteById(movimiento.Id);
                }

                _unitOfWork.CuentaRepository.Delete(cuenta);
                _unitOfWork.Save();
            }
            else
            {
                throw new HttpException("No es posible realizar la operación. Verifique los datos enviados.", "La cuenta es inexistente", 404, System.Net.HttpStatusCode.NotFound);
            }
        }

        public IList<CuentaDTO> GetAll()
        {
            IList<CuentaDTO> ret = new List<CuentaDTO>();
            IList<Cuenta> cuentas = _unitOfWork.CuentaRepository.GetAll();
            foreach (Cuenta cuenta in cuentas)
            {
                CuentaDTO cuentaDTO = CommonMapper.CreateCuentaDTO(cuenta);
                ret.Add(cuentaDTO);
            }
            return ret;
        }

        public CuentaDTO? GetById(int id)
        {
            CuentaDTO? ret = null;

            Cuenta? cuenta = _unitOfWork.CuentaRepository.Find(c => c.Id == id).FirstOrDefault();
            if (cuenta != null)
            {
                ret = CommonMapper.CreateCuentaDTO(cuenta);
            }
            else
            {
                throw new HttpException("No es posible realizar la operación. Verifique los datos enviados.", "La cuenta es inexistente", 404, System.Net.HttpStatusCode.NotFound);
            }

            return ret;
        }

        public IList<CuentaDTO> GetByClientId(int clientId)
        {
            IList<CuentaDTO> cuentasDTO = new List<CuentaDTO>();

            Cliente? cliente = _unitOfWork.ClienteRepository.Find(c => c.Id == clientId).FirstOrDefault();
            if (cliente != null)
            {
                IList<Cuenta> cuentas = _unitOfWork.CuentaRepository.Find(c => c.ClienteId == clientId).ToList();
                foreach (Cuenta cuenta in cuentas)
                {
                    CuentaDTO cuentaDTO = CommonMapper.CreateCuentaDTO(cuenta);
                    cuentasDTO.Add(cuentaDTO);
                }
            }
            else
            {
                throw new HttpException("No es posible realizar la operación. Verifique los datos enviados.", "El cliente es inexistente", 404, System.Net.HttpStatusCode.NotFound);
            }

            return cuentasDTO;
        }

        public CuentaDTO Create(CuentaDTO entity)
        {
            // Mapper
            Cuenta cuenta = CommonMapper.CreateCuenta(entity);

            // Validations
            ValidateCuenta(cuenta, false);

            cuenta.Saldo = cuenta.SaldoInicial;

            _unitOfWork.CuentaRepository.Add(cuenta);
            _unitOfWork.Save();

            return CommonMapper.CreateCuentaDTO(cuenta);
        }

        public CuentaDTO Update(CuentaDTO entity)
        {
            // Mapper
            Cuenta? cuenta = _unitOfWork.CuentaRepository.GetById(entity.Id);
            if (cuenta == null)
            {
                throw new HttpException("No es posible realizar la operación. Verifique los datos enviados.", "La cuenta es inexistente", 400, System.Net.HttpStatusCode.BadRequest);
            }
        
            cuenta = CommonMapper.ModifyCuenta(cuenta, entity);

            // Validations
            ValidateCuenta(cuenta, true);

            _unitOfWork.CuentaRepository.Update(cuenta);
            _unitOfWork.Save();

            return CommonMapper.CreateCuentaDTO(cuenta);
        }

        #endregion Public Methods


        private void ValidateCuenta(Cuenta entity, bool isUpdating)
        {

            string errorMessage = "";
            string errorDetail = "";

            // Valida el Modelo (valida los atributos de las propiedades de la entidad
            List<ValidationResult> vr = new List<ValidationResult>();
            ValidationContext vc = new ValidationContext(entity);
            bool validEntity = false;
            validEntity = Validator.TryValidateObject(entity, vc, vr, true);

            if (!validEntity)
            {
                foreach (ValidationResult item in vr)
                {
                    errorDetail += item.ErrorMessage + ". ";
                }
                errorDetail = errorDetail.Substring(0, errorDetail.Length - 1);
            }
            else
            {
                Cliente? cliente = _unitOfWork.ClienteRepository.GetById(entity.ClienteId);

                if (entity.ClienteId == 0 || cliente == null || cliente.Estado == false)
                {
                    errorDetail = "El cliente es inexistente o bien su estado es 'false'. Recuerde enviar la propiedad 'ClientId' de forma correcta";
                }

                if (isUpdating)
                {
                    if (entity.Id == 0 ||
                        _unitOfWork.CuentaRepository.GetAll().Where(c => c.Id == entity.Id).FirstOrDefault() == null)
                    {
                        errorDetail = "La cuenta es inexistente. Recuerde enviar su Id de forma correcta";
                    }
                    
                    // Chequea que no haya otro Numero de cuenta igual con distinto Id
                    if (entity.Id != 0 && _unitOfWork.CuentaRepository.Find(c => c.Numero == entity.Numero && c.Id != entity.Id).FirstOrDefault() != null)
                    {
                        errorDetail = "Ya existe otra cuenta con el mismo número";
                    }
                }
                else
                {
                    // Chequea que no exista otra cuenta con el mismo numero
                    if (_unitOfWork.CuentaRepository.Find(c => c.Numero == entity.Numero && c.Estado == true).FirstOrDefault() != null)
                    {
                        errorDetail = "Ya existe otra cuenta con el mismo número";
                    }

                    // Verifica si se indicó un Id
                    if (entity.Id != 0)
                    {
                        errorDetail = "No es posible indicar el Id de la cuenta. Déjelo en 0 o bien no lo envíe";
                    }
                }
            }

            if (!string.IsNullOrEmpty(errorDetail))
            {
                errorMessage = "No es posible realizar la operación. Verifique los datos enviados.";
                throw new HttpException(errorMessage, errorDetail, 400, System.Net.HttpStatusCode.BadRequest);
            }

        }
    }
}
