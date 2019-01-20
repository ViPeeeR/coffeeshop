using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShops.Abstract
{
    public interface IRepository<T>
    {
        Task<T> Get(string id);

        Task<T> Add(T item);

        Task Remove(string id);

        Task Update(T item);
    }
}
