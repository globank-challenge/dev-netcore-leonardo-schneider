using BancoEjercicioApi.DataAccess.Repositories;
using BancoEjercicioApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoEjercicioApi.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Vars

        private readonly AppDbContext _dbContext;
        private IRepository<Persona>? _personaRepository;
        private IRepository<Cliente>? _clienteRepository;
        private IRepository<Cuenta>? _cuentaRepository;
        private IRepository<Movimiento>? _movimientoRepository;
        private IRepository<ReporteMovimiento>? _reporteMovimientoRepository;
        private bool _disposed = false;

        #endregion Vars

        #region Constructor

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion Constructos

        #region Public Methods

        #region Repository Properties

        public IRepository<Persona> PersonaRepository
        {
            get
            {
                if (_personaRepository == null)
                {
                    _personaRepository = new Repository<Persona>(_dbContext);
                }
                return _personaRepository;
            }
        }

        public IRepository<Cliente> ClienteRepository
        {
            get 
            {
                if (_clienteRepository == null)
                {
                    _clienteRepository = new Repository<Cliente>(_dbContext);
                }
                return _clienteRepository;
            }
        }

        public IRepository<Cuenta> CuentaRepository
        {
            get
            {
                if (_cuentaRepository == null)
                {
                    _cuentaRepository = new Repository<Cuenta>(_dbContext);
                }
                return _cuentaRepository;
            }
        }

        public IRepository<Movimiento> MovimientoRepository
        {
            get
            {
                
                if (_movimientoRepository == null)
                {
                    _movimientoRepository = new Repository<Movimiento>(_dbContext);
                }
                return _movimientoRepository;
            }
        }

        public IRepository<ReporteMovimiento> ReporteMovimientoRepository
        {
            get
            {
                if (_reporteMovimientoRepository == null)
                {
                    _reporteMovimientoRepository = new Repository<ReporteMovimiento>(_dbContext);
                }
                return _reporteMovimientoRepository;
            }
        }

        #endregion Repository Properties

        /// <summary>
        /// Persiste los cambios del contexto
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
           return _dbContext.SaveChanges();
        }

        /// <summary>
        /// Realiza el dispose del dbContext tambien
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        #endregion Protected Methods
    }
}
