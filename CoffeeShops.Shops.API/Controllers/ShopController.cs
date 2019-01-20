using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShops.Common;
using CoffeeShops.Shops.API.Abstracts;
using CoffeeShops.Shops.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShops.Shops.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopRepository _shopRepository;

        public ShopController(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShopModel>>> Get([FromQuery]int page, [FromQuery]int size)
        {
            var shops = await _shopRepository.GetAll();
            if (page > 0 && size > 0)
            {
                shops = shops.Skip((page - 1) * size).Take(size);
            }

            return Ok(shops.Select(x => new ShopModel()
            {
                Id = x.Id,
                Address = x.Address,
                Name = x.Name,
                Phone = x.Phone,
                Products = x.Products.Select(p => new ProductModel { Id = p.Id, Name = p.Name, Description = p.Description, Price = p.Price })
            }).ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientModel>> Get(string id)
        {
            var shop = await _shopRepository.Get(id);

            if (shop != null)
                return Ok(new ShopModel()
                {
                    Id = shop.Id,
                    Address = shop.Address,
                    Name = shop.Name,
                    Phone = shop.Phone,
                    Products = shop.Products.Select(p => new ProductModel { Id = p.Id, Name = p.Name, Description = p.Description, Price = p.Price })
                });
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ShopModel model)
        {
            var client = new Shop()
            {
                Name = model.Name,
                Address = model.Address,
                Phone = model.Phone
            };

            await _shopRepository.Add(client);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] ShopModel model)
        {
            var client = new Shop()
            {
                Id = id,
                Name = model.Name,
                Address = model.Address,
                Phone = model.Phone
            };

            await _shopRepository.Update(client);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _shopRepository.Remove(id);
            return Ok();
        }
    }
}
