using BancoEjercicioApi.Entities.DTOs;
using BancoEjercicioApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoEjercicioApi.WebApi.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        #region Vars
        
        private readonly IClienteService _clienteService;

        #endregion Vars

        #region Contructor

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        #endregion Contructor

        [HttpPost]
        public IActionResult Post([FromBody] ClienteDTO cliente)
        {
            ClienteDTO ret = _clienteService.Create(cliente);
            return Created("", ret);
        }

        [HttpPut]
        public IActionResult Put([FromBody] ClienteDTO cliente)
        {
            ClienteDTO ret = _clienteService.Update(cliente);
            return Ok(ret);
        }

        [HttpGet]
        public IActionResult Get()
        {
            IList<ClienteDTO> ret = _clienteService.GetAll();
            return Ok(ret);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ClienteDTO? ret = _clienteService.GetById(id);
            return Ok(ret);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _clienteService.DeleteById(id);
            return Ok();
        }
    }
}
