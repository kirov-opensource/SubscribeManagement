using Microsoft.AspNetCore.Mvc;
using SubscribeManagement.WebAPI.Models.Auth;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            return NoContent();
        }
    }
}
