using CoffeeShops.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Services
{
    public interface IOrderService
    {
        Task Create(OrderModel model);

        Task Update(OrderModel model);

        Task<OrderModel> GetById(string id);

        Task<IEnumerable<OrderModel>> GetByClientId(string id);

        Task<IEnumerable<OrderModel>> GetByShopId(string id);

        Task<IEnumerable<OrderModel>> GetAll(int page, int size);

        Task Remove(string id);
    }
}
