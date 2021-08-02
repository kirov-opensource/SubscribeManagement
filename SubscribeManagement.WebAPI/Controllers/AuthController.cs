using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SubscribeManagement.WebAPI.Models;
using SubscribeManagement.WebAPI.Models.Auth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController : Controller
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            return NoContent();
        }
    }
}
