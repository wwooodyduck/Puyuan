using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;

namespace PuyuanDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly RegisterService _registerService;
        

        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            if (register == null)
            {
                return BadRequest("bad");
            }
        var result =await _registerService.Register(register);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> ResgisterComfirm(RegisterComfirmDto registerComfirm)
        {
            if (registerComfirm == null)
            {
                return BadRequest("bad");
            }
            var result = await _registerService.ResgisterComfirm(registerComfirm);
            return result;
        }
    }
}
