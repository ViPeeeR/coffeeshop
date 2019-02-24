using CoffeeShops.Common;
using CoffeeShops.Session.API.Models;
using System.Threading.Tasks;

namespace CoffeeShops.Session.API.Infrastructure
{
    public interface IJwtAuth
    {
        string Create(User user);

        bool Validate(string token);
    }
}
