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

        [HttpPost]
        public async Task<IActionResult> share()
        {

        }
    }
}
