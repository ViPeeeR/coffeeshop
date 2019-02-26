using CoffeeShops.Common;
using CoffeeShops.Orders.API.Abstracts;
using CoffeeShops.Orders.API.Controllers;
using CoffeeShops.Orders.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeShops.Orders.Tests
{
    public class Orders
    {
        [Fact]
        public async Task Order_OkObject_GetAll()
        {
            IEnumerable<Order> orders = new[] { Convert(Model1()), Convert(Model2()) };
            IEnumerable<OrderModel> ordersModel = new[] { Model1(), Model2() };

            var mockRepository = new Mock<IOrderRepository>();
            mockRepository.Setup(x => x.GetAll())
                .Returns(Task.FromResult(orders));

            var controller = new OrderController(mockRepository.Object);

            var result = await controller.Get(1, 1);

            var model = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(1, (model.Value as IEnumerable<OrderModel>)?.Count());
        }

        [Fact]
        public async Task Order_OkObject_Get()
        {
            var order = Convert(Model1());

            var mockRepository = new Mock<IOrderRepository>();
            mockRepository.Setup(x => x.Get(order.Id))
                .Returns(Task.FromResult(order));

            var controller = new OrderController(mockRepository.Object);

            var result = await controller.Get(order.Id);

            var model = Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Order_Ok_Create()
        {
            var order = Convert(Model1());
            var orderModel = Model1();

            var mockRepository = new Mock<IOrderRepository>();
            mockRepository.Setup(x => x.Add(order))
                .Returns(Task.FromResult(order));

            var controller = new OrderController(mockRepository.Object);

            var result = await controller.Post(orderModel);

            var model = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Order_Ok_Update()
        {
            var client = Convert(Model1());
            var clientModel = Model1();

            var mockRepository = new Mock<IOrderRepository>();

            var controller = new OrderController(mockRepository.Object);

            var result = await controller.Put(clientModel.Id, clientModel);

            var model = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Order_Ok_Remove()
        {
            var client = Convert(Model1());
            var clientModel = Model1();

            var mockRepository = new Mock<IOrderRepository>();
            mockRepository.Setup(x => x.Remove(client.Id))
                .Returns(Task.FromResult(client));

            var controller = new OrderController(mockRepository.Object);

            var result = await controller.Delete(clientModel.Id);

            var model = Assert.IsType<OkResult>(result);
        }

        private static OrderModel Model1()
        {
            return new OrderModel()
            {
                Id = "0D34G9F8-0185-4062-B38F-8DE3CFCF3CE9",
                ClientId = "AA7BCB01-C1B1-40EF-A7B9-AF415BEDAEDF",
                ShopId = "8130F6B4-594F-4180-BC1E-C8D8CB986F7C",
                Comment = "Поскорее",
                Date = DateTime.Now,
                DateDelivery = DateTime.Now.AddHours(1),
                StatusOrder = StatusOrder.Delivered,
                StatusPayment = StatusPayment.Paid,
                Products = new[] { new ProductItemModel { Id = "8130G6B4-124F-4180-BC1E-C8D8CB986F7C",
                    Count = 10, ProductId = "617C3827-C44E-44B1-8BB1-10308DE95C1F", OrderId = "0D34G9F8-0185-4062-B38F-8DE3CFCF3CE9" } }
            };
        }

        private static OrderModel Model2()
        {
            return new OrderModel()
            {
                Id = "0D37D9F8-0185-4062-B38F-7DE3CFCF3CE9",
                ClientId = "AA7BCB01-C1B1-40EF-A7B9-AF415BEDAEDF",
                ShopId = "8130F6B4-594F-4180-BC1E-C8D8CB986F7C",
                Comment = "Поскорее",
                Date = DateTime.Now,
                DateDelivery = DateTime.Now.AddHours(2),
                StatusOrder = StatusOrder.Delivered,
                StatusPayment = StatusPayment.Paid,
                Products = new[] { new ProductItemModel { Id = "8130F6B4-124F-4180-BC1E-C8D8CB986F7C",
                    Count = 10, ProductId = "617C3827-C44E-44B1-8BB1-10308DE95C1F", OrderId = "0D37D9F8-0185-4062-B38F-7DE3CFCF3CE9" } }
            };
        }

        private Order Convert(OrderModel model)
        {
            return new Order()
            {
                Id = model.Id,
                ClientId = model.ClientId,
                ShopId = model.ShopId,
                Products = model.Products.Select(x => new ProductItem { Id = x.Id, OrderId = x.OrderId, ProductId = x.ProductId, Count = x.Count}),
                Comment = model.Comment,
                DateDelivery = model.DateDelivery.Value,
                Date = model.Date,
                StatusOrder = model.StatusOrder,
                StatusPayment = model.StatusPayment
            };
        }
    }
}
