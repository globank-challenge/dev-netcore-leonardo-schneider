using BancoEjercicioApi.Abstractions;
using BancoEjercicioApi.DataAccess.UnitOfWork;
using BancoEjercicioApi.Entities;
using BancoEjercicioApi.Entities.DTOs;
using BancoEjercicioApi.Entities.Mappers;
using BancoEjercicioApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoEjercicioApi.Services
{
    public interface IMovimientoService
    {
        void GenerarNuevo(MovimientoDTO movimientoDTO);
    }

    public class MovimientoService : IMovimientoService
    {
        #region Vars

        private readonly IUnitOfWork _unitOfWork;

        #endregion Vars

        #region Constructor

        public MovimientoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        public void GenerarNuevo(MovimientoDTO movimientoDTO)
        {
            ValidarMovimiento(movimientoDTO);

            Movimiento movimiento = CreateMovimiento(movimientoDTO);

            _unitOfWork.MovimientoRepository.Add(movimiento);
            _unitOfWork.CuentaRepository.Update(movimiento.Cuenta);
            _unitOfWork.Save();
        }

        private Movimiento CreateMovimiento(MovimientoDTO movimientoDTO)
        {
            
            Movimiento movimiento = new Movimiento();
            movimiento.Cuenta = _unitOfWork.CuentaRepository.GetById(movimientoDTO.CuentaId);
            movimiento.CuentaId = movimientoDTO.CuentaId;

            try
            {
                DateTime dateTime = DateTime.ParseExact(movimientoDTO.Fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                movimiento.Fecha = dateTime;
            }
            catch (Exception)
            {
                string errorMessage = "No es posible realizar la operación. Verifique los datos enviados.";
                throw new HttpException(errorMessage, "La fecha del movimiento debe estar en formato DD/MM/YYYY", 400, System.Net.HttpStatusCode.BadRequest);
            }

            movimiento.Valor = movimientoDTO.Valor;
            movimiento.SaldoInicial = movimiento.Cuenta.Saldo;
            movimiento.SaldoDisponible = movimiento.SaldoInicial + movimiento.Valor;
            movimiento.Tipo = (movimiento.Valor) < 0 ? "Débito" : "Crédito";

            // Actualiza el saldo actual de la cuenta
            movimiento.Cuenta.Saldo += movimiento.Valor;

            return movimiento;
        }

        private void ValidarMovimiento(MovimientoDTO movimientoDTO)
        {
            string errorMessage = "No es posible realizar la operación. Verifique los datos enviados.";
            
            if (movimientoDTO.CuentaId == 0)
            {
                throw new HttpException(errorMessage, "Debe indicar la 'CuentaId'", 400, System.Net.HttpStatusCode.BadRequest);
            }

            Cuenta? cuenta = _unitOfWork.CuentaRepository.GetById(movimientoDTO.CuentaId);
            if (cuenta == null)
            {
                throw new HttpException(errorMessage, "La cuenta indicada es inexistente", 400, System.Net.HttpStatusCode.BadRequest);
            }

            if (movimientoDTO.Valor < 0)
            {
                if (cuenta.Saldo < Math.Abs(movimientoDTO.Valor))
                {
                    throw new HttpException(errorMessage, "Saldo no disponible", 400, System.Net.HttpStatusCode.BadRequest);
                }
            }
        }

    }
}
