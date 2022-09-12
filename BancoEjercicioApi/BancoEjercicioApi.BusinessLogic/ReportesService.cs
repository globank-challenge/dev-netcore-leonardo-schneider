using BancoEjercicioApi.Abstractions;
using BancoEjercicioApi.DataAccess.UnitOfWork;
using BancoEjercicioApi.Entities;
using BancoEjercicioApi.Entities.DTOs;
using BancoEjercicioApi.Entities.Mappers;
using BancoEjercicioApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoEjercicioApi.Services
{
    public interface IReportesService
    {   IList<ReporteMovimientoDTO> GetReporteEstadoDeCuenta(int? clienteId, DateTime? fechaDesde, DateTime? fechaHasta);
    }

    public class ReportesService : IReportesService
    {
        #region Vars

        private readonly IUnitOfWork _unitOfWork;

        #endregion Vars

        #region Constructor

        public ReportesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        public IList<ReporteMovimientoDTO> GetReporteEstadoDeCuenta(int? clienteId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            IList<ReporteMovimientoDTO> ret = new List<ReporteMovimientoDTO>();

            IList<ReporteMovimiento> reporteMovimientos = _unitOfWork.ReporteMovimientoRepository.GetFromSQLString($"exec [dbo].spReporteMovimiento @ClienteId={clienteId}, @FechaDesde={fechaDesde}, @FechaHasta={fechaHasta}");
            foreach (ReporteMovimiento reporteMovimiento in reporteMovimientos)
            {
                ret.Add(CommonMapper.CreateReporteMovimientoDTO(reporteMovimiento));
            }

            return ret;
        }
    }
}
