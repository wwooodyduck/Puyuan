using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PuyuanDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class VerificationController : ControllerBase
    {
        private readonly VerificationService _verificationService;

        public VerificationController(VerificationService verificationService)
        {
            _verificationService = verificationService;
        }
        [HttpPost("send")]
        public async Task<IActionResult>SendVerification(SendVerificationDto sendVerification)
        {
            if (sendVerification == null)
            {
                return BadRequest("檢查輸入資料");
            }
            var result = await _verificationService.SendVerification(sendVerification);
            return result;
        }


        [HttpPost("check")]
        public async Task<IActionResult> CheckVerification(CheckVerificationDto checkVerification)
        {
            if (checkVerification == null)
            {
                return BadRequest("檢查輸入資料");
            }
            var result = await _verificationService.CheckVerification(checkVerification);
            return result;
        }
    }
}
