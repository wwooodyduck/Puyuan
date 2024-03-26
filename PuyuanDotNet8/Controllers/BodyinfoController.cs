using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuyuanDotNet8.Data;
using PuyuanDotNet8.Dtos;
using PuyuanDotNet8.Services;

namespace PuyuanDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyinfoController : ControllerBase
    {
        private readonly BodyinfoServices _bodyinfoServices;
        public BodyinfoController(BodyinfoServices bloodPressure)
        {
            _bodyinfoServices = bloodPressure;
        }
        [HttpPost("bloodpressure")]
        public async Task<IActionResult>BloodPressureUpload(BodyDto Bloodpressure)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (Bloodpressure == null)
            {
                return BadRequest();
            }
            var result = await _bodyinfoServices.BloodPressureUpload(Bloodpressure, uuid);
            return result;
        }
        [HttpPost("weight")]
        public async Task<IActionResult> WeightUpload(WeightDto Weightdto)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (Weightdto == null)
            {
                return BadRequest();
            }
            var result = await _bodyinfoServices.WeightUpload(Weightdto, uuid);
            return result;
        }
        [HttpPost("bloodsugar")]
        public async Task<IActionResult>BloodSugar(BloodSugarDto bloodSugar)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (bloodSugar == null)
            {
                return BadRequest();
            }
            var result = await _bodyinfoServices.BloodSugar(bloodSugar, uuid);
            return result;
        }
        [HttpPost("HbA1c")]
        public async Task<IActionResult>HbA1cUpload(HbA1cDto hbA1Cdto)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (hbA1Cdto == null)
            {
                return BadRequest();
            }
            var result = await _bodyinfoServices.HbA1cUpload(hbA1Cdto, uuid);
            return result;
        }
        [HttpGet("HbA1c")]
        public async Task<IActionResult> HbA1cGet()
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result= await _bodyinfoServices.HbA1cGet(uuid);
            return result; 
        }
        [HttpDelete("HbA1c")]
        public async Task<IActionResult> HbA1cDelete(HbA1cDelete hbA1Cdelete)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _bodyinfoServices.HbA1cDelete(hbA1Cdelete,uuid);
            return result;
        }
    }
}
