using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PuyuanDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class shareController : ControllerBase
    {
        private readonly shareServices _shareServices;
        public shareController(shareServices shareServices)
        {
            _shareServices = shareServices;
        }

        [HttpGet("{type}")]
        public async Task<IActionResult>shareget(string type)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            JsonResult success = new JsonResult(new { status = "0", message = "success", records=new Object[0] });
            JsonResult fail = new JsonResult(new { status = "1", message = "fail", records = new Object[0] });
            switch(int.Parse(type))
            {
                case 0:
                    return success;
                case 1:
                    return success;
                case 2:
                    return success;
                default: 
                    return fail;
            }
        }
    }
}
