using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShops.Common;
using CoffeeShops.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShops.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<AuthenticateModel>> Login([FromBody]LoginModel model)
        {
            var token = await _authService.Login(model);
            return Ok(token);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> AuthCode([FromQuery]string clientId)
        {
            var url = await _authService.AuthCode(clientId);
            return Ok("http://example.ru" + url);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> LoginApp([FromQuery]string clientId, [FromQuery]string clientSecret, [FromQuery]string code)
        {
            var token = await _authService.Login(clientId, clientSecret, code);
            return Ok(token);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Refresh([FromBody]string refreshToken)
        {
            var token = await _authService.Refresh(new AuthenticateModel { RefreshToken = refreshToken });
            return Ok(token);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<AuthenticateModel>> Register([FromBody]RegisterModel model)
        {
            var token = await _authService.Register(model);
            return Ok(token);
        }
    }
}