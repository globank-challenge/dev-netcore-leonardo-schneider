using BancoEjercicioApi.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoEjercicioApi.Entities.Mappers
{
    public static class CommonMapper
    {
        #region Cliente
        public static Cliente CreateCliente(ClienteDTO origen)
        {
            Cliente destino = new Cliente();

            destino.Id = origen.ClienteId;
            destino.Estado = origen.Estado;
            destino.Edad = origen.Edad;
            destino.Identificacion = origen.Identificacion;
            destino.Direccion = origen.Direccion;
            destino.Genero = origen.Genero;
            destino.Nombre = origen.Nombre;
            destino.Password = origen.Password;
            destino.Telefono = origen.Telefono;
            
            return destino;
        }

        public static Cliente ModifyCliente(Cliente destino, ClienteDTO origen)
        {
            destino.Estado = origen.Estado;
            destino.Edad = origen.Edad;
            destino.Identificacion = origen.Identificacion;
            destino.Direccion = origen.Direccion;
            destino.Genero = origen.Genero;
            destino.Nombre = origen.Nombre;
            destino.Password = origen.Password;
            destino.Telefono = origen.Telefono;

            return destino;
        }


        public static ClienteDTO CreateClienteDTO(Cliente origen)
        {
            ClienteDTO destino = new ClienteDTO();

            destino.ClienteId = origen.Id;
            destino.Estado = origen.Estado;
            destino.Edad = origen.Edad;
            destino.Identificacion = origen.Identificacion;
            destino.Direccion = origen.Direccion;
            destino.Genero = origen.Genero;
            destino.Nombre = origen.Nombre;
            destino.Password = origen.Password;
            destino.Telefono = origen.Telefono;

            return destino;
        }

#endregion Cliente

        #region Cuenta

        public static Cuenta CreateCuenta(CuentaDTO origen)
        {
            Cuenta destino = new Cuenta();

            destino.ClienteId = origen.ClienteId;
            destino.Estado = origen.Estado;
            destino.Tipo = origen.Tipo;
            destino.Numero = origen.Numero;
            destino.SaldoInicial = origen.SaldoInicial;
            
            return destino;
        }

        public static Cuenta ModifyCuenta(Cuenta destino, CuentaDTO origen)
        {
            destino.ClienteId = origen.ClienteId;
            destino.Estado = origen.Estado;
            destino.Tipo = origen.Tipo;
            destino.Numero = origen.Numero;
            destino.SaldoInicial = origen.SaldoInicial;

            return destino;
        }

        public static CuentaDTO CreateCuentaDTO(Cuenta origen)
        {
            CuentaDTO destino = new CuentaDTO();

            destino.Id = origen.Id;
            destino.ClienteId = origen.ClienteId;
            destino.Estado = origen.Estado;
            destino.Tipo = origen.Tipo;
            destino.Numero = origen.Numero;
            destino.SaldoInicial = origen.SaldoInicial;

            return destino;
        }

        #endregion Cuenta

        #region ReporteMovimiento

        public static ReporteMovimientoDTO CreateReporteMovimientoDTO(ReporteMovimiento origen)
        {
            ReporteMovimientoDTO destino = new ReporteMovimientoDTO();

            destino.Tipo = origen.Tipo;
            destino.Estado = origen.Estado;
            destino.Fecha = origen.Fecha.ToString("dd/MM/yyyy");
            destino.Movimiento = origen.Movimiento;
            destino.Nombre = origen.Nombre;
            destino.Numero = origen.Numero;
            destino.SaldoDisponible = origen.SaldoDisponible;
            destino.SaldoInicial = origen.SaldoInicial;

            return destino;
        }

        #endregion ReporteMovimiento
    }
}
