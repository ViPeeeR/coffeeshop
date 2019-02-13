using System.Threading.Tasks;
using CoffeeShops.Common;
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
                return BadRequest("Не заполнены все обязательные поля!");
            }

            await _productService.Create(model);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]ProductModel model)
        {
            if (string.IsNullOrEmpty(model.Name) ||
                string.IsNullOrEmpty(model.Description) ||
                model.Price == 0m ||
                string.IsNullOrEmpty(model.ShopId))
            {
                return BadRequest("Не заполнены все обязательные поля!");
            }

            await _productService.Update(model);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]string shopId, [FromQuery]int page, [FromQuery]int size)
        {
            var products = await _productService.GetAll(shopId, 0, 0);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var product = await _productService.GetById(id);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(string id)
        {
            await _productService.Remove(id);
            return Ok();
        }
    }
}
