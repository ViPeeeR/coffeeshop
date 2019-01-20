using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShops.Common;
using CoffeeShops.Users.API.Abstracts;
using CoffeeShops.Users.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShops.Users.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientModel>>> Get([FromQuery]int page, [FromQuery]int size)
        {
            var clients = await _clientRepository.GetAll();
            if (page > 0 && size > 0)
            {
                clients = clients.Skip((page - 1) * size).Take(size);
            }

            return Ok(clients.Select(x => new ClientModel()
            {
                Id = x.Id,
                Age = x.Age,
                FirstName = x.FirstName,
                LastName = x.LastName,
                MiddleName = x.MiddleName,
                Phone = x.Phone,
                Sex = x.Sex
            }).ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientModel>> Get(string id)
        {
            var client = await _clientRepository.Get(id);

            if (client != null)
                return Ok(new ClientModel()
                {
                    Id = client?.Id,
                    Age = client?.Age,
                    FirstName = client?.FirstName,
                    LastName = client?.LastName,
                    MiddleName = client?.MiddleName,
                    Phone = client?.Phone,
                    Sex = client?.Sex
                });
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClientModel model)
        {
            var client = new Client()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Age = model.Age,
                Phone = model.Phone,
                Sex = model.Sex
            };

            await _clientRepository.Add(client);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] ClientModel model)
        {
            var client = new Client()
            {
                Id = id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Age = model.Age,
                Phone = model.Phone,
                Sex = model.Sex
            };

            await _clientRepository.Update(client);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _clientRepository.Remove(id);
            return Ok();
        }
    }
}
