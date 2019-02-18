using CoffeeShops.Common;
using CoffeeShops.Session.API.Models;
using System.Threading.Tasks;

namespace CoffeeShops.Session.API.Infrastructure
{
    public interface IJwtAuth
    {
        AuthenticateModel CreateToken(User user);

        bool ValidateAccess(string token);

        Task<AuthenticateModel> ValidateRefresh(string token);

        string AuthorizationCode(string clientId);

        Task<AuthenticateModel> CreateToken(string code, string clientSecret, string clientId);
    }
}
