using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Services
{
    public interface IAuthServicesService
    {
        Task AuthClient();

        Task AuthShop();

        Task AuthOrder();
    }
}
