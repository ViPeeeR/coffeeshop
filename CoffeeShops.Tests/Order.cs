using CoffeeShops.Common;
using CoffeeShops.Controllers;
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
    public class Order
    {
        [Fact]
        public async Task Order_Ok_Create()
        {
            OrderModel orderModel = Model();

            var mockOrder = new Mock<IOrderService>();
            var controller = new OrderController(mockOrder.Object, null, null);

            var result = await controller.Create(orderModel);

            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Order_Ok_Update()
        {
            OrderModel orderModel = Model();

            var mockOrder = new Mock<IOrderService>();
            var controller = new OrderController(mockOrder.Object, null, null);

            var result = await controller.Update(orderModel);

            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Order_OkObject_GetAll()
        {
            OrderModel orderModel = Model();

            var mockOrder = new Mock<IOrderService>();
            var mockClient = new Mock<IClientService>();
            var mockShop = new Mock<IShopService>();

            mockOrder.Setup(x => x.GetById(orderModel.Id))
                .Returns(Task.FromResult(orderModel));

            mockClient.Setup(x => x.GetById(orderModel.ClientId))
                .Returns(Task.FromResult(null as ClientModel));

            mockShop.Setup(x => x.GetById(orderModel.ShopId))
                .Returns(Task.FromResult(null as ShopModel));

            var controller = new OrderController(mockOrder.Object, null, null);

            var result = await controller.Get(1, 10);

            var actionResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Order_OkObject_Get()
        {
            OrderModel orderModel = Model();

            var mockOrder = new Mock<IOrderService>();
            var mockClient = new Mock<IClientService>();
            var mockShop = new Mock<IShopService>();

            mockOrder.Setup(x => x.GetById(orderModel.Id))
                .Returns(Task.FromResult(orderModel));

            mockClient.Setup(x => x.GetById(orderModel.ClientId))
                .Returns(Task.FromResult(null as ClientModel));

            mockShop.Setup(x => x.GetById(orderModel.ShopId))
                .Returns(Task.FromResult(null as ShopModel));

            var controller = new OrderController(mockOrder.Object, null, null);

            var result = await controller.Get(orderModel.Id);

            var actionResult = Assert.IsType<OkObjectResult>(result);
        }

        private static OrderModel Model()
        {
            return new OrderModel()
            {
                ClientId = "3862ABFB-2AB0-4D89-94A1-01AEB4E427AF",
                ShopId = "658CF412-678C-473C-9D36-9E2AA04948EB",
                StatusOrder = StatusOrder.Prepares,
                StatusPayment = StatusPayment.Cash,
                Products = new[] { new ProductItemModel() { Id = "qweqwe", Count = 2 } },
                Date = DateTime.Now,
                DateDelivery = DateTime.Now.AddHours(2)
            };
        }
    }
}
