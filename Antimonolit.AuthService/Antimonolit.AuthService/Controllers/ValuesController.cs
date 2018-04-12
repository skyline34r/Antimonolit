using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antimonolith.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Antimonolit.AuthService.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ValuesController : Controller
    {
        private IAuthService authService;
        public ValuesController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public object Verify([FromBody] TokenModel tokens)
        {
            return authService.GetData(tokens.AccessToken);
        }
    }
}
