using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        public object Verify(string token)
        {
            return authService.GetData(token);
        }
    }
}
