using System;
using System.Threading.Tasks;
using CoffeeShops.Common;
using CoffeeShops.Infrustructure;
using CoffeeShops.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShops.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]ProductModel model)
        {
            if (string.IsNullOrEmpty(model.Name) ||
                string.IsNullOrEmpty(model.Description) ||
                model.Price == 0m ||
                string.IsNullOrEmpty(model.ShopId))
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = "Не заполнены все обязательные поля" });
            }

            try
            {
                await _productService.Create(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]ProductModel model)
        {
            if (string.IsNullOrEmpty(model.Name) ||
                string.IsNullOrEmpty(model.Description) ||
                model.Price == 0m ||
                string.IsNullOrEmpty(model.ShopId))
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = "Не заполнены все обязательные поля" });
            }

            try
            {
                await _productService.Update(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]string shopId, [FromQuery]int page, [FromQuery]int size)
        {
            try
            {
                var products = await _productService.GetAll(shopId, 0, 0);
                return Ok(products);
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
                var product = await _productService.GetById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(string id)
        {
            try
            {
                await _productService.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseError() { ErrorCode = 400, Message = ex.Message });
            }
        }
    }
}
