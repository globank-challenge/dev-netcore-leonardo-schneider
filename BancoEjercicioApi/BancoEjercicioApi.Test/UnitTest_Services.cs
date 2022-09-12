using BancoEjercicioApi.DataAccess.UnitOfWork;
using BancoEjercicioApi.Entities.DTOs;
using BancoEjercicioApi.Exceptions;
using BancoEjercicioApi.Services;

namespace BancoEjercicioApi.Test
{
    public class UnitTest_Services
    {
        [Fact]
        public void Test_Cliente_Nuevo_Ok()
        {
            ClienteDTO clienteDTO = new ClienteDTO();
            clienteDTO.Nombre = "Juan Pedro Aznar";
            clienteDTO.Edad = 38;
            clienteDTO.Direccion = "Constitucion 1245, San Fernando, Buenos Aires, Argentina";
            clienteDTO.Identificacion = "123456789";
            clienteDTO.Genero = "Masculino";
            clienteDTO.Telefono = "1130228976";
            clienteDTO.Password = "12345";

            ClienteService cs = new ClienteService(new UnitOfWorkMock());
            ClienteDTO resp = cs.Create(clienteDTO);
            Assert.True(resp.ClienteId != 0);
        }

        [Fact]
        public void Test_Cliente_Nuevo_Identificacion_Invalida()
        {
            ClienteDTO clienteDTO = new ClienteDTO();
            clienteDTO.Nombre = "Juan Pedro Aznar";
            clienteDTO.Edad = 35;
            clienteDTO.Direccion = "Constitucion 1245, San Fernando, Buenos Aires, Argentina";
            clienteDTO.Identificacion = "123456789JKLHHGDAFHGADFMNNMNAMSD";
            clienteDTO.Genero = "Masculino";
            clienteDTO.Telefono = "1130228976";
            clienteDTO.Password = "12345";

            ClienteService cs = new ClienteService(new UnitOfWorkMock());
            HttpException exception = Assert.Throws<HttpException>(() => cs.Create(clienteDTO));
            Assert.Equal("La identificacion debe ser de hasta 20 caracteres.", exception.ErrorDetail);
        }

        [Fact]
        public void Test_Crear_Cuenta_y_Movimiento_Ok()
        {
            UnitOfWorkMock unitOfWorkMock = new UnitOfWorkMock();

            #region Creacion del Cliente

            ClienteDTO clienteDTO = new ClienteDTO();
            clienteDTO.Nombre = "Juan Pedro Aznar";
            clienteDTO.Edad = 38;
            clienteDTO.Direccion = "Constitucion 1245, San Fernando, Buenos Aires, Argentina";
            clienteDTO.Identificacion = "123456789";
            clienteDTO.Genero = "Masculino";
            clienteDTO.Telefono = "1130228976";
            clienteDTO.Password = "12345";
            clienteDTO.Estado = true;

            ClienteService cs = new ClienteService(unitOfWorkMock);
            ClienteDTO resp = cs.Create(clienteDTO);
            Assert.True(resp.ClienteId != 0); // Creo el cliente

            #endregion Creacion del Cliente

            #region Creacion de la Cuenta

            CuentaDTO cuentaDTO = new CuentaDTO();
            cuentaDTO.ClienteId = resp.ClienteId;
            cuentaDTO.Numero = "123456";
            cuentaDTO.Estado = true;
            cuentaDTO.SaldoInicial = 1000;
            cuentaDTO.Tipo = "Ahorro";

            CuentaService cus = new CuentaService(unitOfWorkMock);
            CuentaDTO respCuenta = cus.Create(cuentaDTO);
            Assert.True(respCuenta.Id != 0);

            #endregion Creacion de la Cuenta

            #region Creacion del Movimiento

            MovimientoDTO movimientoDTO = new MovimientoDTO();
            movimientoDTO.CuentaId = respCuenta.Id;
            movimientoDTO.Valor = 500;
            movimientoDTO.Fecha = "01/08/2022";

            MovimientoService ms = new MovimientoService(unitOfWorkMock);
            bool movimientoGenerado = false;
            try
            {
                ms.GenerarNuevo(movimientoDTO);
                movimientoGenerado = true;
            }
            catch (Exception)
            {
            }
            
            Assert.True(movimientoGenerado);

            #endregion Creacion del Movimiento
        }

        [Fact]
        public void Test_Movimiento_Con_Saldo_Insuficiente()
        {
            UnitOfWorkMock unitOfWorkMock = new UnitOfWorkMock();

            #region Creacion del Cliente

            ClienteDTO clienteDTO = new ClienteDTO();
            clienteDTO.Nombre = "Juan Pedro Aznar";
            clienteDTO.Edad = 38;
            clienteDTO.Direccion = "Constitucion 1245, San Fernando, Buenos Aires, Argentina";
            clienteDTO.Identificacion = "123456789";
            clienteDTO.Genero = "Masculino";
            clienteDTO.Telefono = "1130228976";
            clienteDTO.Password = "12345";
            clienteDTO.Estado = true;

            ClienteService cs = new ClienteService(unitOfWorkMock);
            ClienteDTO resp = cs.Create(clienteDTO);
            Assert.True(resp.ClienteId != 0); // Creo el cliente

            #endregion Creacion del Cliente

            #region Creacion de la Cuenta

            CuentaDTO cuentaDTO = new CuentaDTO();
            cuentaDTO.ClienteId = resp.ClienteId;
            cuentaDTO.Numero = "123456";
            cuentaDTO.Estado = true;
            cuentaDTO.SaldoInicial = 1000;
            cuentaDTO.Tipo = "Ahorro";

            CuentaService cus = new CuentaService(unitOfWorkMock);
            CuentaDTO respCuenta = cus.Create(cuentaDTO);
            Assert.True(respCuenta.Id != 0);

            #endregion Creacion de la Cuenta

            #region Creacion del Movimiento

            MovimientoDTO movimientoDTO = new MovimientoDTO();
            movimientoDTO.CuentaId = respCuenta.Id;
            movimientoDTO.Valor = -5000;
            movimientoDTO.Fecha = "01/08/2022";

            MovimientoService ms = new MovimientoService(unitOfWorkMock);
            HttpException exception = Assert.Throws<HttpException>(() => ms.GenerarNuevo(movimientoDTO));
            Assert.Equal("Saldo no disponible", exception.ErrorDetail);
        }

        #endregion Creacion del Movimiento
    }
}