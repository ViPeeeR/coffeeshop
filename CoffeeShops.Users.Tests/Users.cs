using CoffeeShops.Common;
using CoffeeShops.Users.API.Abstracts;
using CoffeeShops.Users.API.Controllers;
using CoffeeShops.Users.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeShops.Users.Tests
{
    public class Users
    {
        [Fact]
        public async Task User_OkObject_GetAll()
        {
            IEnumerable<Client> clients = new[] { Convert(Model1()), Convert(Model2()) };
            IEnumerable<ClientModel> clientsModel = new[] { Model1(), Model2() };

            var mockRepository = new Mock<IClientRepository>();
            mockRepository.Setup(x => x.GetAll())
                .Returns(Task.FromResult(clients));

            var controller = new ClientController(mockRepository.Object, null);

            var result = await controller.Get(1, 1);

            var model = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(1, (model.Value as IEnumerable<ClientModel>)?.Count());
        }

        [Fact]
        public async Task User_OkObject_Get()
        {
            var client = Convert(Model1());

            var mockRepository = new Mock<IClientRepository>();
            mockRepository.Setup(x => x.Get(client.Id))
                .Returns(Task.FromResult(client));

            var controller = new ClientController(mockRepository.Object, null);

            var result = await controller.Get(client.Id);

            var model = Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task User_OkObject_Create()
        {
            var client = Convert(Model1());
            var clientModel = Model1();

            var mockRepository = new Mock<IClientRepository>();
            mockRepository.Setup(x => x.Add(client))
                .Returns(Task.FromResult(client));

            var controller = new ClientController(mockRepository.Object, null);

            var result = await controller.Post(clientModel);

            var model = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task User_OkObject_Update()
        {
            var client = Convert(Model1());
            var clientModel = Model1();

            var mockRepository = new Mock<IClientRepository>();

            var controller = new ClientController(mockRepository.Object, null);

            var result = await controller.Put(clientModel);

            var model = Assert.IsType<OkResult>(result);
        }

        private static ClientModel Model1()
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

        private static ClientModel Model2()
        {
            return new ClientModel()
            {
                Id = "0D37D9F8-0185-4062-B38F-7DE3CFCF3CE9",
                FirstName = "Вадим",
                LastName = "Онучин",
                MiddleName = "Александрович",
                Birthday = DateTime.Now,
                Phone = "79990008811"
            };
        }

        private Client Convert(ClientModel model)
        {
            return new Client()
            {
                Id = model.Id,
                Birthday = model.Birthday,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Phone = model.Phone,
                Sex = model.Sex
            };
        }
    }
}
