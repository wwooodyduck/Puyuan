using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PuyuanDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class newsController : ControllerBase
    {
        private readonly newsservices _newsService;

        public newsController(newsservices newsService)
        {
            _newsService = newsService;
        }
        [HttpGet]
        public async Task<IActionResult> news()
        {

        }
    }
}
