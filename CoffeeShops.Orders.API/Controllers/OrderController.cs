using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShops.Common;
using CoffeeShops.Orders.API.Abstracts;
using CoffeeShops.Orders.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShops.Orders.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> Get([FromQuery]int page, [FromQuery]int size)
        {
            var orders = await _orderRepository.GetAll();
            if (page > 0 && size > 0)
            {
                orders = orders.Skip((page - 1) * size).Take(size);
            }

            return Ok(orders.Select(x => new OrderModel()
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Comment = x.Comment,
                Date = x.Date,
                DateDelivery = x.DateDelivery,
                Products = x.Products?.Select(p => new ProductItemModel { Id = p.Id, ProductId = p.ProductId, Count = p.Count }),
                ShopId = x.ShopId,
                StatusOrder = x.StatusOrder,
                StatusPayment = x.StatusPayment
            }).ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> Get(string id)
        {
            var order = await _orderRepository.Get(id);

            if (order != null)
                return Ok(new OrderModel()
                {
                    Id = order.Id,
                    ClientId = order.ClientId,
                    Comment = order.Comment,
                    Date = order.Date,
                    DateDelivery = order.DateDelivery,
                    Products = order.Products.Select(p => new ProductItemModel { Id = p.Id, ProductId = p.ProductId, Count = p.Count }),
                    ShopId = order.ShopId,
                    StatusOrder = order.StatusOrder,
                    StatusPayment = order.StatusPayment
                });

            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrderModel model)
        {
            var order = new Order()
            {
                ClientId = model.ClientId,
                Comment = model.Comment,
                Date = model.Date,
                DateDelivery = model.DateDelivery ?? DateTime.UtcNow.AddHours(1),
                ShopId = model.ShopId,
                StatusOrder = model.StatusOrder,
                StatusPayment = model.StatusPayment
            };
            var result = await _orderRepository.Add(order);

            var products = model.Products.Select(p => new ProductItem { ProductId = p.ProductId, Count = p.Count, OrderId = result.Id });
            await _orderRepository.AddProducts(products);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] OrderModel model)
        {
            var order = new Order()
            {
                Id = id,
                ClientId = model.ClientId,
                Comment = model.Comment,
                Date = model.Date,
                DateDelivery = model.DateDelivery.Value,
                ShopId = model.ShopId,
                StatusOrder = model.StatusOrder,
                StatusPayment = model.StatusPayment
            };

            await _orderRepository.Update(order);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _orderRepository.Remove(id);
            return Ok();
        }

        [HttpGet("client/{id}")]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetByClient(string id)
        {
            var orders = await _orderRepository.GetByClientId(id);

            return Ok(orders?.Select(x => new OrderModel()
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Comment = x.Comment,
                Date = x.Date,
                DateDelivery = x.DateDelivery,
                Products = x.Products?.Select(p => new ProductItemModel { Id = p.Id, ProductId = p.ProductId, Count = p.Count }),
                ShopId = x.ShopId,
                StatusOrder = x.StatusOrder,
                StatusPayment = x.StatusPayment
            }).ToList());
        }

        [HttpGet("shop/{id}")]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetByShop(string id)
        {
            var orders = await _orderRepository.GetByShopId(id);

            return Ok(orders?.Select(x => new OrderModel()
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Comment = x.Comment,
                Date = x.Date,
                DateDelivery = x.DateDelivery,
                Products = x.Products?.Select(p => new ProductItemModel { Id = p.Id, ProductId = p.ProductId, Count = p.Count }),
                ShopId = x.ShopId,
                StatusOrder = x.StatusOrder,
                StatusPayment = x.StatusPayment
            }).ToList());
        }
    }
}
