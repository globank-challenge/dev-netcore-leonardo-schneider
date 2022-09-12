using BancoEjercicioApi.Entities.DTOs;
using BancoEjercicioApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoEjercicioApi.WebApi.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        #region Vars
        
        private readonly ICuentaService _cuentaService;

        #endregion Vars

        #region Contructor

        public CuentaController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        #endregion Contructor

        [HttpPost]
        public IActionResult Post([FromBody] CuentaDTO cuenta)
        {
            CuentaDTO ret = _cuentaService.Create(cuenta);
            return Created("", ret);
        }

        [HttpPut]
        public IActionResult Put([FromBody] CuentaDTO cuenta)
        {
            CuentaDTO ret = _cuentaService.Update(cuenta);
            return Ok(ret);
        }

        [HttpGet]
        public IActionResult Get()
        {
            IList<CuentaDTO> ret = _cuentaService.GetAll();
            return Ok(ret);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            CuentaDTO? ret = _cuentaService.GetById(id);
            return Ok(ret);
        }
        

        
        [HttpGet("GetByClientId/{clientId}")]
        public IActionResult GetByClientId(int clientId)
        {
            IList<CuentaDTO> ret = _cuentaService.GetByClientId(clientId);
            return Ok(ret);
        }
        

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _cuentaService.DeleteById(id);
            return Ok();
        }
    }
}
