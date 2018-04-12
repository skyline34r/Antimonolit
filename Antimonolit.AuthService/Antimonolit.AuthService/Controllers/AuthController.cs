using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antimonolith.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Antimonolith.AuthService.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth/[action]")]
    public class AuthController : Controller
    {
        private IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public TokenModel Login([FromBody] LoginModel loginModel)
        {
            return authService.GetToken(loginModel.Login, loginModel.Password);
        }

        [HttpPost]
        public TokenModel Refresh([FromBody] TokenModel tokens)
        {
            return authService.RenewToken(tokens.RefreshToken);
        }

        [HttpPost]
        public IActionResult Logout([FromBody] TokenModel tokens)
        {
            authService.RemoveRefreshToken(tokens.RefreshToken);
            return Ok();
        }
    }

    public class LoginModel
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}