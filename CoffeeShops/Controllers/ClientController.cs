using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShops.Common;
using CoffeeShops.Infrustructure;
using CoffeeShops.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShops.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;

        public ClientController(IClientService clientService,
            IOrderService orderService,
            IAuthService authService)
        {
            _clientService = clientService;
            _orderService = orderService;
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]ClientModel model)
        {
            if (!await _authService.Validate(Request))
                return Unauthorized(new ResponseError() { ErrorCode = 401, Message = "Unauthorized" });

            if (string.IsNullOrEmpty(model.FirstName) ||
                string.IsNullOrEmpty(model.LastName) ||
                string.IsNullOrEmpty(model.MiddleName) ||
                string.IsNullOrEmpty(model.Phone) ||
                model.Birthday == DateTime.MinValue)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = "Не заполнены все обязательные поля" });
            }

            try
            {
                await _clientService.Create(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]ClientModel model)
        {
            if (!await _authService.Validate(Request))
                return Unauthorized(new ResponseError() { ErrorCode = 401, Message = "Unauthorized" });

            if (string.IsNullOrEmpty(model.FirstName) ||
                string.IsNullOrEmpty(model.LastName) ||
                string.IsNullOrEmpty(model.MiddleName) ||
                string.IsNullOrEmpty(model.Phone) ||
                model.Birthday == DateTime.MinValue)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = "Не заполнены все обязательные поля" });
            }

            try
            {
                await _clientService.Update(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]int page, [FromQuery]int size)
        {
            if (!await _authService.Validate(Request))
                return Unauthorized(new ResponseError() { ErrorCode = 401, Message = "Unauthorized" });

            try
            {
                var clients = await _clientService.GetAll(0, 0);
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            if (!await _authService.Validate(Request))
                return Unauthorized(new ResponseError() { ErrorCode = 401, Message = "Unauthorized" });

            try
            {
                var client = await _clientService.GetById(id);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (!await _authService.Validate(Request))
                return Unauthorized(new ResponseError() { ErrorCode = 401, Message = "Unauthorized" });

            try
            {
                var orders = await _orderService.GetByClientId(id);
                foreach (var order in orders)
                {
                    await _orderService.Remove(order.Id);
                }

                await _clientService.Remove(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = ex.Message });
            }
        }
    }
}
