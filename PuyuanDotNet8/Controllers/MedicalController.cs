using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuyuanDotNet8.Services;

namespace PuyuanDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalController : ControllerBase
    {
        private readonly MedicalServices _medicalServices;
        public MedicalController(MedicalServices medicalServices)
        {
            _medicalServices = medicalServices;
        }

        [HttpGet("medcialget")]
        public async Task<IActionResult> MedcialGet()
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _medicalServices.MedcialGet(uuid);
            return result;
        }

        [HttpPatch]
        public async Task<IActionResult> MedcialUpdate(MedicalDto MedicalDto)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;

            if (MedicalDto == null)
            {
                return BadRequest("bad");
            }

            var result = await _medicalServices.MedcialUpdate(MedicalDto,uuid);
            return result;
        }
    }
}
