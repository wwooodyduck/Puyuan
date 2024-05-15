using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuyuanDotNet8.Services;

namespace PuyuanDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class newsController : ControllerBase
    {
        private readonly NewsServices _newsService;

        public newsController(NewsServices newsService)
        {
            _newsService = newsService;
        }
        [HttpGet]
        public async Task<IActionResult> news()
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _newsService.news(uuid);
            return result;
        }
    }
}
