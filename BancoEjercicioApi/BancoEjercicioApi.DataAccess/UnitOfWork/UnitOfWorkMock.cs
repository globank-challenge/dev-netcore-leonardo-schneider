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
    public class UnitOfWorkMock : IUnitOfWork
    {
        #region Vars

        private IRepository<Persona>? _personaRepository;
        private IRepository<Cliente>? _clienteRepository;
        private IRepository<Cuenta>? _cuentaRepository;
        private IRepository<Movimiento>? _movimientoRepository;
        private IRepository<ReporteMovimiento>? _reporteMovimientoRepository;
        
        #endregion Vars

        #region Constructor

        public UnitOfWorkMock()
        {
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
                    _personaRepository = new RepositoryMock<Persona>();
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
                    _clienteRepository = new RepositoryMock<Cliente>();
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
                    _cuentaRepository = new RepositoryMock<Cuenta>();
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
                    _movimientoRepository = new RepositoryMock<Movimiento>();
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
                    _reporteMovimientoRepository = new RepositoryMock<ReporteMovimiento>();
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
           return 1;
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
        }

        #endregion Protected Methods
    }
}
