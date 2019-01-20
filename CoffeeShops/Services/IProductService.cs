using CoffeeShops.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Services
{
    public interface IProductService
    {
        Task Create(ProductModel model);

        Task Update(ProductModel model);

        Task<IEnumerable<ProductModel>> GetAll(string shopId, int page, int size);

        Task Remove(string id);
    }
}
