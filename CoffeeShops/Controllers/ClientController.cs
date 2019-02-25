using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeShops.Common;
using CoffeeShops.Infrustructure;
using CoffeeShops.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoffeeShops.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService,
            IOrderService orderService,
            IAuthService authService, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _orderService = orderService;
            _authService = authService;
            _logger = logger;
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
                var client = await _clientService.Remove(id);
                if (client == null)
                    return BadRequest(new ResponseError() { ErrorCode = 400, Message = "Не найден такой клиент" });

                try
                {
                    // TODO: если после этого ошибка, то надо откатить (вернуть сейчас клиента, а потом его обратно добавить)
                    await _orderService.RemoveByClientId(id);
                }
                catch (HttpRequestException)
                {
                    await _clientService.Create(client);
                    return StatusCode(403, new ResponseError() { ErrorCode = 403, Message = "Сервер не доступен" });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = ex.Message });
            }
        }
    }
}
