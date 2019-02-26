using CoffeeShops.Common;
using CoffeeShops.Shops.API.Abstracts;
using CoffeeShops.Shops.API.Controllers;
using CoffeeShops.Shops.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeShops.Shops.Tests
{
    public class Shops
    {
        [Fact]
        public async Task User_OkObject_GetAll()
        {
            IEnumerable<Shop> clients = new[] { Convert(Model1()), Convert(Model2()) };
            IEnumerable<ShopModel> clientsModel = new[] { Model1(), Model2() };

            var mockRepository = new Mock<IShopRepository>();
            mockRepository.Setup(x => x.GetAll())
                .Returns(Task.FromResult(clients));

            var controller = new ShopController(mockRepository.Object);

            var result = await controller.Get(1, 1);

            var model = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(1, (model.Value as IEnumerable<ShopModel>)?.Count());
        }

        [Fact]
        public async Task User_OkObject_Get()
        {
            var client = Convert(Model1());

            var mockRepository = new Mock<IShopRepository>();
            mockRepository.Setup(x => x.Get(client.Id))
                .Returns(Task.FromResult(client));

            var controller = new ShopController(mockRepository.Object);

            var result = await controller.Get(client.Id);

            var model = Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task User_Ok_Create()
        {
            var client = Convert(Model1());
            var clientModel = Model1();

            var mockRepository = new Mock<IShopRepository>();
            mockRepository.Setup(x => x.Add(client))
                .Returns(Task.FromResult(client));

            var controller = new ShopController(mockRepository.Object);

            var result = await controller.Post(clientModel);

            var model = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task User_Ok_Update()
        {
            var client = Convert(Model1());
            var clientModel = Model1();

            var mockRepository = new Mock<IShopRepository>();

            var controller = new ShopController(mockRepository.Object);

            var result = await controller.Put(clientModel);

            var model = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task User_Ok_Remove()
        {
            var client = Convert(Model1());
            var clientModel = Model1();

            var mockRepository = new Mock<IShopRepository>();
            mockRepository.Setup(x => x.Remove(client.Id))
                .Returns(Task.FromResult(client));

            var controller = new ShopController(mockRepository.Object);

            var result = await controller.Delete(clientModel.Id);

            var model = Assert.IsType<OkResult>(result);
        }

        private static ShopModel Model1()
        {
            return new ShopModel()
            {
                Id = "6D37D9F8-0085-4062-B38F-7DE3CFCF3CE9",
                Address = "г. Москва",
                Name = "Плюшки и ватрушки",
                Products = new[] { new ProductModel { Id = "A2E5195D-6821-405D-B755-B703F09310D8", Name = "Кофе", Price = 150, ShopId = "6D37D9F8-0085-4062-B38F-7DE3CFCF3CE9" } },
                Phone = "79680115072"
            };
        }

        private static ShopModel Model2()
        {
            return new ShopModel()
            {
                Id = "0D37D9F8-0185-4062-B38F-7DE3CFCF3CE9",
                Address = "г. Москва, пр-кт Измайловский 73/2",
                Name = "СушиWok",
                Products = new[] { new ProductModel { Id = "A2E5195D-6821-155D-B755-B703F09310D8", Name = "Суши", Price = 350, ShopId = "0D37D9F8-0185-4062-B38F-7DE3CFCF3CE9" } },
                Phone = "79990008811"
            };
        }

        private Shop Convert(ShopModel model)
        {
            return new Shop()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Products = model.Products.Select(x => new Product { Id = x.Id, Name = x.Name, Price = x.Price, ShopId = x.ShopId }),
                Phone = model.Phone,
            };
        }
    }
}
