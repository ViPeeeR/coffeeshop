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
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("{shopId}")]
        public async Task<ActionResult<IEnumerable<ShopModel>>> Get(string shopId, [FromQuery]int page, [FromQuery]int size)
        {
            var shops = await _productRepository.GetAll(shopId);
            if (page > 0 && size > 0)
            {
                shops = shops.Skip((page - 1) * size).Take(size);
            }

            return Ok(shops.Select(x => new ProductModel()
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name, 
                Price = x.Price,
                ShopId = x.ShopId
            }).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductModel model)
        {
            var product = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ShopId = model.ShopId
            };

            await _productRepository.Add(product);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] ProductModel model)
        {
            var client = new Product()
            {
                Id = id,
                Description = model.Description,
                Price = model.Price,
            };

            await _productRepository.Update(client);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _productRepository.Remove(id);
            return Ok();
        }
    }
}
