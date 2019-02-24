using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShops.Common;
using CoffeeShops.Shops.API.Abstracts;
using CoffeeShops.Shops.API.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]string shopId, [FromQuery]int page, [FromQuery]int size)
        {
            var products = await _productRepository.GetAll(shopId);
            if (page > 0 && size > 0)
            {
                products = products.Skip((page - 1) * size).Take(size);
            }

            return Ok(products.Select(x => new ProductModel()
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name, 
                Price = x.Price,
                ShopId = x.ShopId
            }).ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var product = await _productRepository.Get(id);
            if (product != null)
                return Ok(new ProductModel()
                {
                    Id = product.Id,
                    Price = product.Price,
                    Name = product.Name,
                    Description = product.Description,
                    ShopId = product.ShopId
                });
            else
                return NotFound();
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

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProductModel model)
        {
            var product = new Product()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ShopId = model.ShopId
            };

            await _productRepository.Update(product);
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
