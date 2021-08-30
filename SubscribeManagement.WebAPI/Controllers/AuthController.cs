using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="redirectUri">回调地址,相对路径/开头</param>
        /// <returns></returns>
        [HttpGet("githubsignin")]
        public async Task<IActionResult> GitHubSignIn(string redirectUri)
        {
            if (string.IsNullOrWhiteSpace(redirectUri))
                redirectUri = "/";
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUri }, "GitHub");
        }
    }
}
