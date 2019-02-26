using CoffeeShops.Common;
using CoffeeShops.Controllers;
using CoffeeShops.Infrustructure;
using CoffeeShops.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeShops.Tests
{
    public class Shop
    {
        [Fact]
        public async Task Shop_Ok_Create()
        {
            var shopModel = Model();

            var mockShop = new Mock<IShopService>();

            var controller = new ShopController(mockShop.Object, null, null);

            var result = await controller.Create(shopModel);

            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Shop_Ok_Delete()
        {
            var shopModel = Model();

            var mockShop = new Mock<IShopService>();
            var mockOrder = new Mock<IOrderService>();
            var mockQueueServices = new Mock<QueueServices>();

            mockShop.Setup(x => x.Remove(shopModel.Id))
                .Returns(Task.FromResult(shopModel));

            mockShop.Setup(x => x.Check())
                .Returns(Task.FromResult(true));

            mockOrder.Setup(x => x.Check())
                .Returns(Task.FromResult(true));

            var controller = new ShopController(mockShop.Object, mockOrder.Object, mockQueueServices.Object);

            var result = await controller.Delete(shopModel.Id);

            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Shop_Ok_Put()
        {
            var shopModel = Model();

            var mockShop = new Mock<IShopService>();

            var controller = new ShopController(mockShop.Object, null, null);

            var result = await controller.Update(shopModel);

            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Shop_OkObject_GetAll()
        {
            var shopModel = Model();

            IEnumerable<ShopModel> shops = new[] { shopModel };

            var mockShop = new Mock<IShopService>();

            mockShop.Setup(x => x.GetAll(1, 10))
                .Returns(Task.FromResult(shops));

            var controller = new ShopController(mockShop.Object, null, null);

            var result = await controller.Get(1, 10);

            var actionResult = Assert.IsType<OkObjectResult>(result);
        }

        private static ShopModel Model()
        {
            return new ShopModel()
            {
                Id = "6D37D9F8-0085-4062-B38F-7DE3CFCF3CE9",
                Phone = "79680115072",
                Address = "г. Москва",
                Name = "Плюшки и игрушки",
                Products = new[] { new ProductModel() { Id = "qweqwe", Name = "Кофе", Price = 123 } }
            };
        }
    }
}
