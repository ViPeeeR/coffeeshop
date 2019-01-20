using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShops.Common;
using CoffeeShops.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShops.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]ClientModel model)
        {
            await _clientService.Create(model);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<ClientModel>> Get([FromQuery]int page, [FromQuery]int size)
        {
            var clients = await _clientService.GetAll(0, 0);
            return Ok(clients);
        }
    }
}
