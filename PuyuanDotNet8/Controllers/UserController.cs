using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PuyuanDotNet8.Data;
using PuyuanDotNet8.Dtos;
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

        [HttpPut("badge")]
        public async Task<IActionResult> BadgeUpdate(BadgeUpdateDto badgeUpdate)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (badgeUpdate == null)
            {
                return BadRequest();
            }
            var result = await _usersetService.BadgeUpdate(badgeUpdate, uuid);
            return result;
        }
        [HttpGet("lastupdate")]
        public async Task<IActionResult> LastUpdate()
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _usersetService.LastUpdate(uuid);
            return result;
        }
        [HttpPost("lastrecord")]
        public async Task<IActionResult> lastrecorded(LastRecordDto lastRecord)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (lastRecord == null)
            {
                return BadRequest("bad");
            }
            var result = await _usersetService.lastrecorded(lastRecord, uuid);
            return result;
        }
        [HttpGet("userinfo")]
        public async Task<IActionResult> Userinfo()
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _usersetService.Userinfo(uuid);
            return result;
        }
    }
}
