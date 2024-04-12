using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PuyuanDotNet8.Services;

namespace PuyuanDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly AuthService _authService;

        public authController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            if (login == null)
            {
                return BadRequest();
            }
            var result = await _authService.Login(login);
            return result;
        }
    }

    
}
