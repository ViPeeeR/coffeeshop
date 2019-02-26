using CoffeeShops.Common;
using CoffeeShops.Controllers;
using CoffeeShops.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeShops.Tests
{
    public class Client
    {
        [Fact]
        public async Task Client_Ok_Create()
        {
            var clientModel = Model();

            var mockClient = new Mock<IClientService>();
            var mockAuth = new Mock<IAuthService>();

            mockAuth.Setup(c => c.Validate(null as HttpRequest))
                .Returns(Task.FromResult(true));

            var controller = new ClientController(mockClient.Object, null, mockAuth.Object, null);

            var result = await controller.Create(clientModel);

            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Client_Ok_Delete()
        {
            var clientModel = Model();

            var mockClient = new Mock<IClientService>();
            var mockOrder = new Mock<IOrderService>();
            var mockAuth = new Mock<IAuthService>();

            mockClient.Setup(x => x.Remove(clientModel.Id))
                .Returns(Task.FromResult(clientModel));

            mockAuth.Setup(c => c.Validate(null as HttpRequest))
                .Returns(Task.FromResult(true));

            var controller = new ClientController(mockClient.Object, mockOrder.Object, mockAuth.Object, null);

            var result = await controller.Delete(clientModel.Id);

            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Client_Ok_Put()
        {
            var clientModel = Model();

            var mockClient = new Mock<IClientService>();
            var mockOrder = new Mock<IOrderService>();
            var mockAuth = new Mock<IAuthService>();

            mockAuth.Setup(c => c.Validate(null as HttpRequest))
                .Returns(Task.FromResult(true));

            var controller = new ClientController(mockClient.Object, null, mockAuth.Object, null);

            var result = await controller.Update(clientModel);

            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Client_OkObject_GetAll()
        {
            var clientModel = Model();

            IEnumerable<ClientModel> clients = new[] { clientModel };

            var mockClient = new Mock<IClientService>();
            var mockOrder = new Mock<IOrderService>();
            var mockAuth = new Mock<IAuthService>();

            mockClient.Setup(x => x.GetAll(1, 10))
                .Returns(Task.FromResult(clients));

            mockAuth.Setup(c => c.Validate(null as HttpRequest))
                .Returns(Task.FromResult(true));

            var controller = new ClientController(mockClient.Object, null, mockAuth.Object, null);

            var result = await controller.Get(1, 10);

            var actionResult = Assert.IsType<OkObjectResult>(result);
        }

        private static ClientModel Model()
        {
            return new ClientModel()
            {
                Id = "6D37D9F8-0085-4062-B38F-7DE3CFCF3CE9",
                FirstName = "Илья",
                LastName = "Онучин",
                MiddleName = "Александрович",
                Birthday = DateTime.Now,
                Phone = "79680115072"
            };
        }
    }
}
