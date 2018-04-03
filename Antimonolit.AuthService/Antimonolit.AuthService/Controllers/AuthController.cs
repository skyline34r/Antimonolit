using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Antimonolith.AuthService.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public string Auth([FromBody] LoginModel loginModel)
        {
            return authService.GetToken(loginModel.Login, loginModel.Password);
        }
    }

    public class LoginModel
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}