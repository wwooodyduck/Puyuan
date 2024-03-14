﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuyuanDotNet8.Services;

namespace PuyuanDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgetpasswordController : ControllerBase
    {
        private readonly ForgetPasswordService _forgotPasswordService;

        public ForgetpasswordController(ForgetPasswordService forgotPasswordService)
        {
            _forgotPasswordService = forgotPasswordService;
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(SendVerificationDto forgets)
        {
            if (forgets == null)
            {
                return BadRequest();
            }
            var result = await _forgotPasswordService.ForgotPassword(forgets);
            return result;
        }

        [HttpPost("check")]
        public async Task<IActionResult>ResetPassword(ResetPasswordDto resetPassword)
        {

        }
    }
}
