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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]OrderModel model)
        {
            model.Date = DateTime.UtcNow;
            await _orderService.Create(model);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]OrderModel model)
        {
            await _orderService.Update(model);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]int page, [FromQuery]int size)
        {
            var orders = await _orderService.GetAll(0, 0);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var order = await _orderService.GetById(id);
            return Ok(order);
        }
    }
}
