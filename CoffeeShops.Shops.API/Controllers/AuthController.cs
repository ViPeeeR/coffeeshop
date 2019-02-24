using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeShops.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeShops.Shops.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("[action]")]
        public ActionResult<AuthServiceToken> Login([FromQuery]string appId, [FromQuery]string appSecret)
        {
            if (appId == "shop_api" && appSecret == "key_shop_api")
            {
                var jwt = new JwtSecurityToken(
                issuer: "cs_shop_api",
                audience: "gateway-cs-app",
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(TimeSpan.FromSeconds(1800)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("key_shop_api")),
                    SecurityAlgorithms.HmacSha256
                ));

                var token = new JwtSecurityTokenHandler().WriteToken(jwt);
                return Ok(new AuthServiceToken()
                {
                    Token = token,
                    Create = DateTime.UtcNow.Second,
                    Lifespan = 1800
                });
            }

            return Forbid();
        }
    }
}