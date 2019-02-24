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

namespace CoffeeShops.Users.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("[action]")]
        public ActionResult<AuthServiceToken> Login([FromQuery]string appId, [FromQuery]string appSecret)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            if (appId == "user_api" && appSecret == "key_user_api")
            {
                var jwt = new JwtSecurityToken(
                issuer: "cs-user-api",
                audience: "gateway-cs-app",
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(TimeSpan.FromSeconds(1800)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("keyuserapikeyuserapikeyuserapi")),
                    SecurityAlgorithms.HmacSha256
                ));

                var token = new JwtSecurityTokenHandler().WriteToken(jwt);
                return Ok(new AuthServiceToken()
                {
                    Token = token,
                    Create = (int)(DateTime.UtcNow - origin).TotalSeconds,
                    Lifespan = 1800
                });
            }

            return Forbid();
        }
    }
}