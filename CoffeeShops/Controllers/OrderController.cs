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
        private readonly IClientService _clientService;
        private readonly IShopService _shopService;

        public OrderController(IOrderService orderService,
            IClientService clientService,
            IShopService shopService)
        {
            _orderService = orderService;
            _clientService = clientService;
            _shopService = shopService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]OrderModel model)
        {
            if (string.IsNullOrEmpty(model.ShopId) ||
                string.IsNullOrEmpty(model.ClientId) ||
                model.Products.Count() == 0)
            {
                return BadRequest("Не заполнены обязательные поля!");
            }

            model.Date = DateTime.UtcNow;
            await _orderService.Create(model);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]OrderModel model)
        {
            if (string.IsNullOrEmpty(model.ShopId) ||
                string.IsNullOrEmpty(model.ClientId) ||
                model.Products.Count() == 0 ||
                !model.DateDelivery.HasValue ||
                model.Date == DateTime.MinValue)
            {
                return BadRequest("Не заполнены обязательные поля!");
            }

            await _orderService.Update(model);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]int page, [FromQuery]int size)
        {
            var orders = await _orderService.GetAll(0, 0);

            foreach (var order in orders)
            {
                order.Client = await _clientService.GetById(order.ClientId);
                order.Shop = await _shopService.GetById(order.ShopId);
            }

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var order = await _orderService.GetById(id);
            order.Client = await _clientService.GetById(order.ClientId);
            order.Shop = await _shopService.GetById(order.ShopId);
            return Ok(order);
        }
    }
}
