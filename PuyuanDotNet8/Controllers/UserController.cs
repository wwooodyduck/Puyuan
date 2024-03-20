﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PuyuanDotNet8.Data;
using PuyuanDotNet8.Services;

namespace PuyuanDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    { 
        private readonly UsersetService _usersetService;
        public UserController(UsersetService registerService)
        {
            _usersetService = registerService;
        }
        [HttpPatch("UserSet")]
        public async Task<IActionResult>UserSet(UsersetDto userset)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (userset == null) 
            {
                return BadRequest();
            }
            var result = await _usersetService.UserSet(userset,uuid);
            return result;
        }

        [HttpPatch("UserDefault")]
        public async Task<IActionResult>UserDefault(UserDefaultDto userDefault)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if(userDefault==null)
            {
                return BadRequest();
            }                                                               
            var result= await _usersetService.UserDefault(userDefault,uuid);
            return result;
        }

        [HttpPatch("setting")]
        public async Task<IActionResult> Setting(SettingDto setting)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (setting == null)
            {
                return BadRequest();
            }
            var result = await _usersetService.Setting(setting,uuid);
            return result;
        }
    }
}
