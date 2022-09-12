using BancoEjercicioApi.Entities.DTOs;
using BancoEjercicioApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoEjercicioApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        #region Vars

        private readonly IMovimientoService _movimientoService;

        #endregion Vars

        #region Contructor

        public MovimientoController(IMovimientoService movimientoService)
        {
            _movimientoService = movimientoService;
        }

        #endregion Contructor

        [HttpPost]
        public IActionResult Post([FromBody] MovimientoDTO movimientoDTO)
        {
            _movimientoService.GenerarNuevo(movimientoDTO);
            return Ok();
        }
    }
}
