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
        public async Task<ActionResult> AuthCode([FromQuery]string client_id, [FromQuery]string response_type = "code")
        {
            var url = await _authService.AuthCode(client_id);
            return Ok("http://example.ru" + url);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> LoginApp([FromQuery]string client_id, [FromQuery]string client_secret, 
            [FromQuery]string code, [FromQuery]string grant_type = "authorization_code")
        {
            var token = await _authService.Login(client_id, client_secret, code);
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