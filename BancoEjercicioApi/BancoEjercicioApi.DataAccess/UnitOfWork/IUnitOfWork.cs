using BancoEjercicioApi.DataAccess.Repositories;
using BancoEjercicioApi.Entities;

namespace BancoEjercicioApi.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IRepository<Persona> PersonaRepository { get; }
        public IRepository<Cliente> ClienteRepository { get; }
        public IRepository<Cuenta> CuentaRepository { get; }
        public IRepository<Movimiento> MovimientoRepository { get; }
        public IRepository<ReporteMovimiento> ReporteMovimientoRepository { get; }
        public int Save();
        public void Dispose();
    }
}
