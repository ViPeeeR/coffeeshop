using CoffeeShops.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Services
{
    public interface IClientService
    {
        Task Create(ClientModel clientModel);

        Task Update(ClientModel clientModel);

        Task<ClientModel> GetById(string id);

        Task<IEnumerable<ClientModel>> GetAll(int page, int size);

        Task Remove(string id);
    }
}
