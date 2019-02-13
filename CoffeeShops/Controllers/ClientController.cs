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
            if (string.IsNullOrEmpty(model.FirstName) ||
                string.IsNullOrEmpty(model.LastName) ||
                string.IsNullOrEmpty(model.MiddleName) ||
                string.IsNullOrEmpty(model.Phone) ||
                model.Birthday == DateTime.MinValue)
            {
                return BadRequest("Не заполнены все обязательные поля");
            }

            await _clientService.Create(model);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]ClientModel model)
        {
            if (string.IsNullOrEmpty(model.FirstName) ||
                string.IsNullOrEmpty(model.LastName) ||
                string.IsNullOrEmpty(model.MiddleName) ||
                string.IsNullOrEmpty(model.Phone) ||
                model.Birthday == DateTime.MinValue)
            {
                return BadRequest("Не заполнены все обязательные поля");
            }

            await _clientService.Update(model);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]int page, [FromQuery]int size)
        {
            var clients = await _clientService.GetAll(0, 0);
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var client = await _clientService.GetById(id);
            return Ok(client);
        }
    }
}
