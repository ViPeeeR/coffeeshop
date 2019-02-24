using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShops.Common;
using CoffeeShops.Session.API.Abstracts;
using CoffeeShops.Session.API.Infrastructure;
using CoffeeShops.Session.API.Infrustucture;
using CoffeeShops.Session.API.Infrustucture.Providers;
using CoffeeShops.Session.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoffeeShops.Session.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ILogger<SessionController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IJwtAuth _jwtAuth;
        private readonly IPasswordProvider _passwordProvider;
        private readonly IOAuth _oauth;

        public SessionController(ILogger<SessionController> logger,
            IUserRepository userRepository,
            IJwtAuth jwtAuth,
            IPasswordProvider passwordProvider,
            IOAuth oauth)
        {
            _logger = logger;
            _userRepository = userRepository;
            _jwtAuth = jwtAuth;
            _passwordProvider = passwordProvider;
            _oauth = oauth;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<AuthenticateModel>> Login([FromBody]LoginModel model)
        {
            var user = await _userRepository.GetByLogin(model.Login);
            if (_passwordProvider.VerifyHashedPassword(user.PassHash, user.ContactId, model.Password))
            {
                var token = _oauth.CreateToken(user);
                return Ok(token);
            }

            return Forbid();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<AuthenticateModel>> Register([FromBody]RegisterModel model)
        {
            var user = new User
            {
                ContactId = model.ContactId,
                Login = model.Login,
                PassHash = _passwordProvider.GetHash(model.Password, model.ContactId),
                Role = model.Role
            };

            user = await _userRepository.Add(user);
            var token = _oauth.CreateToken(user);

            return Ok(token);
        }

        [HttpPost("[action]")]
        public ActionResult Verify([FromBody]AuthenticateModel model)
        {
            if (_jwtAuth.Validate(model.AccessToken))
                return Ok();

            return Forbid();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<AuthenticateModel>> Refresh([FromBody]AuthenticateModel model)
        {
            var token = await _oauth.ValidateRefresh(model.RefreshToken);
            if (token != null)
                return Ok(token);

            return Forbid();
        }

        [HttpGet("[action]")]
        public ActionResult<string> Code([FromQuery]string clientId)
        {
            var code = _oauth.AuthorizationCode(clientId);
            if (code != null)
                return Ok(code);

            return Forbid();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<AuthenticateModel>> OAuth([FromQuery]string clientId, [FromQuery]string code, [FromQuery]string clientSecret)
        {
            var token = await _oauth.CreateToken(code, clientSecret, clientId);
            if (token != null)
                return Ok(token);

            return Forbid();
        }
    }
}
