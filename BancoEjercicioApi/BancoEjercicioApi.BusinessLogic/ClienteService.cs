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
    public interface IClienteService : ICRUD<ClienteDTO>
    { 
    }

    public class ClienteService : IClienteService
    {
        #region Vars

        private readonly IUnitOfWork _unitOfWork;

        #endregion Vars

        #region Constructor

        public ClienteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        #region Public Methods

        public void DeleteById(int id)
        {
            Cliente? cliente = _unitOfWork.ClienteRepository.Find(c => c.Id == id).FirstOrDefault();
            if (cliente != null)
            {
                // Elimina todas las cuentas
                IList<Cuenta> cuentasVinculadas = _unitOfWork.CuentaRepository.Find(c => c.ClienteId == id).ToList();
                foreach (Cuenta cuenta in cuentasVinculadas)
                {
                    // Elimina todos los movimientos
                    IList<Movimiento> movimientosVinculados = _unitOfWork.MovimientoRepository.Find(c => c.CuentaId == cuenta.Id).ToList();
                    foreach (Movimiento movimiento in movimientosVinculados)
                    {
                        _unitOfWork.MovimientoRepository.DeleteById(movimiento.Id);
                    }

                    _unitOfWork.CuentaRepository.DeleteById(cuenta.Id);
                }

                _unitOfWork.ClienteRepository.Delete(cliente);
                _unitOfWork.Save();
            }
            else
            {
                throw new HttpException("No es posible realizar la operación. Verifique los datos enviados.", "El cliente es inexistente", 404, System.Net.HttpStatusCode.NotFound);
            }
        }

        public IList<ClienteDTO> GetAll()
        {
            IList<ClienteDTO> ret = new List<ClienteDTO>();
            IList<Cliente> clientes = _unitOfWork.ClienteRepository.GetAll();
            foreach (Cliente cliente in clientes)
            {
                ClienteDTO clienteDTO = CommonMapper.CreateClienteDTO(cliente);
                ret.Add(clienteDTO);
            }
            return ret;
        }

        public ClienteDTO? GetById(int id)
        {
            ClienteDTO? ret = null;

            Cliente? cliente = _unitOfWork.ClienteRepository.Find(c => c.Id == id).FirstOrDefault();
            if (cliente != null)
            {
                ret = CommonMapper.CreateClienteDTO(cliente);
            }
            else
            {
                throw new HttpException("No es posible realizar la operación. Verifique los datos enviados.", "El cliente es inexistente", 404, System.Net.HttpStatusCode.NotFound);
            }

            return ret;
        }

        public ClienteDTO Create(ClienteDTO entity)
        {
            // Mapper
            Cliente cliente = CommonMapper.CreateCliente(entity);

            // Validations
            ValidateClient(cliente, false);

            _unitOfWork.ClienteRepository.Add(cliente);
            _unitOfWork.Save();

            return CommonMapper.CreateClienteDTO(cliente);
        }

        public ClienteDTO Update(ClienteDTO entity)
        {
            // Mapper
            Cliente? cliente = _unitOfWork.ClienteRepository.GetById(entity.ClienteId);
            if (cliente == null)
            {
                throw new HttpException("No es posible realizar la operación. Verifique los datos enviados.", "El cliente es inexistente", 400, System.Net.HttpStatusCode.BadRequest);
            }
        
            cliente = CommonMapper.ModifyCliente(cliente, entity);

            // Validations
            ValidateClient(cliente, true);

            _unitOfWork.ClienteRepository.Update(cliente);
            _unitOfWork.Save();

            return CommonMapper.CreateClienteDTO(cliente);
        }

        #endregion Public Methods


        private void ValidateClient(Cliente entity, bool isUpdating)
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
                // Aca se podria agregar algo particular
                if (isUpdating)
                {
                    if (entity.Id == 0 ||
                        _unitOfWork.ClienteRepository.GetAll().Where(c => c.Id == entity.Id).FirstOrDefault() == null)
                    {
                        errorDetail = "El cliente es inexistente. Recuerde enviar su Id de forma correcta";
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
