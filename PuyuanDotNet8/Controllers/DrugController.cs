using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuyuanDotNet8.Dtos;
using PuyuanDotNet8.Services;

namespace PuyuanDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        private readonly DrugServices _drugServices;
        public DrugController(DrugServices drugServices)
        {
            _drugServices = drugServices;
        }
        [HttpGet]
        public async Task<IActionResult>Druginfoget([FromQuery] DrugDto drugget)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;

            if (drugget == null)
            {
                return BadRequest("bad");
            }

            var result = await _drugServices.Druginfoget(drugget, uuid);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> DruginfoUpload(DrugUploadDto drugUpload)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (drugUpload == null)
            {
                return BadRequest("bad");
            }

            var result = await _drugServices.DruginfoUpload(drugUpload, uuid);
            return result;
        }
        [HttpDelete]
        public async Task<IActionResult> DrugInfoDelete(DrugDeleteDto drugDelete)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _drugServices.DrugInfoDelete(drugDelete, uuid);
            return result;
        }
    }
}
