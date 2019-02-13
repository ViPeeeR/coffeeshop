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
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]ShopModel model)
        {
            if (string.IsNullOrEmpty(model.Address) ||
                string.IsNullOrEmpty(model.Name) ||
                string.IsNullOrEmpty(model.Phone))
            {
                return BadRequest("Не заполнены все обязательные поля!");
            }


            await _shopService.Create(model);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]ShopModel model)
        {
            if (string.IsNullOrEmpty(model.Address) ||
                string.IsNullOrEmpty(model.Name) ||
                string.IsNullOrEmpty(model.Phone))
            {
                return BadRequest("Не заполнены все обязательные поля!");
            }

            await _shopService.Update(model);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]int page, [FromQuery]int size)
        {
            var shops = await _shopService.GetAll(0, 0);
            return Ok(shops);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var shop = await _shopService.GetById(id);
            return Ok(shop);
        }
    }
}
