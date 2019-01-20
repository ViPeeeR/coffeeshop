using CoffeeShops.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Services
{
    public interface IShopService
    {
        Task Create(ShopModel model);

        Task Update(ShopModel model);

        Task<ShopModel> GetById(string id);

        Task<IEnumerable<ShopModel>> GetAll(int page, int size);

        Task Remove(string id);
    }
}
