﻿using System;
using System.Collections.Generic;
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
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly IOrderService _orderService;
        private readonly QueueServices _queueServices;

        public ShopController(IShopService shopService,
            IOrderService orderService,
            QueueServices queueServices)
        {
            _shopService = shopService;
            _orderService = orderService;
            _queueServices = queueServices;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]ShopModel model)
        {
            if (string.IsNullOrEmpty(model.Address) ||
                string.IsNullOrEmpty(model.Name) ||
                string.IsNullOrEmpty(model.Phone))
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = "Не заполнены все обязательные поля" });
            }

            try
            {
                await _shopService.Create(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]ShopModel model)
        {
            if (string.IsNullOrEmpty(model.Address) ||
                string.IsNullOrEmpty(model.Name) ||
                string.IsNullOrEmpty(model.Phone))
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = "Не заполнены все обязательные поля" });
            }

            try
            {
                await _shopService.Update(model);
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
            try
            {
                var shops = await _shopService.GetAll(0, 0);
                return Ok(shops);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            try
            {
                var shop = await _shopService.GetById(id);
                return Ok(shop);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                if (await _orderService.Check())
                    await _orderService.RemoveByShopId(id);
                else
                    _queueServices.Order.Add(async () =>
                    {
                        await _orderService.RemoveByShopId(id);
                    });

                if (await _shopService.Check())
                    await _shopService.Remove(id);
                else
                    _queueServices.Shop.Add(async () =>
                    {
                        await _shopService.Remove(id);
                    });

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = ex.Message });
            }
        }
    }
}
