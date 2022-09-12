using BancoEjercicioApi.Entities.DTOs;
using BancoEjercicioApi.Exceptions;
using BancoEjercicioApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BancoEjercicioApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        #region Vars

        private readonly IReportesService _reportesService;

        #endregion Vars

        #region Contructor

        public ReportesController(IReportesService reportesService)
        {
            _reportesService = reportesService;
        }

        #endregion Contructor

        [HttpGet("EstadoDeCuenta")]
        public IActionResult EstadoDeCuenta([FromQuery] int? clienteId, [FromQuery] string? fechaDesde, [FromQuery] string? fechaHasta)
        {
            DateTime? dtDesde = null;
            DateTime? dtHasta = null;

            try
            {
                if (!string.IsNullOrEmpty(fechaDesde))
                {
                    dtDesde = DateTime.ParseExact(fechaDesde, "ddMMyyyy", CultureInfo.InvariantCulture);
                }
    
                if (!string.IsNullOrEmpty(fechaHasta))
                {
                    dtHasta = DateTime.ParseExact(fechaHasta, "ddMMyyyy", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception)
            {
                string errorMessage = "No es posible realizar la operación. Verifique los datos enviados.";
                throw new HttpException(errorMessage, "Las fechas deben estar en formato ddMMyyyy", 400, System.Net.HttpStatusCode.BadRequest);
            }

            IList<ReporteMovimientoDTO> ret = _reportesService.GetReporteEstadoDeCuenta(clienteId, dtDesde, dtHasta);
            return Ok(ret);
        }
    }
}
