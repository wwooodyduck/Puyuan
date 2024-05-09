using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PuyuanDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class friendController : ControllerBase
    {
        private readonly FriendServices _friendService;

        public friendController(FriendServices friendService)
        {
            _friendService = friendService;
        }
        [HttpGet("code")]
        public async Task<IActionResult>codeget()
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _friendService.codeget(uuid);
            return result;
        }
        [HttpGet("list")]
        public async Task<IActionResult> groupteam()
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _friendService.groupteam(uuid);
            return result;
        }

        [HttpGet("requests")]
        public async Task<IActionResult> teaminvited()
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _friendService.teaminvited(uuid);
            return result;
        }
    }
}
