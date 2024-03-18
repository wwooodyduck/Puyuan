using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPatch]
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
    }
}
