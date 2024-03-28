using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuyuanDotNet8.Data;
using PuyuanDotNet8.Dtos;
using PuyuanDotNet8.Services;

namespace PuyuanDotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyDietController : ControllerBase
    {
        private readonly DailyDietServices _dailyDietServices;
        public DailyDietController(DailyDietServices dailyDiet)
        {
            _dailyDietServices = dailyDiet;
        }
        [HttpPost]
        public async Task<IActionResult>DailyDietuploald(DailyDietDto dailyDietDto)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (dailyDietDto == null)
            {
                return BadRequest();
            }
            var result = await _dailyDietServices.DailyDietuploald(dailyDietDto, uuid);
            return result;
        }

        [HttpDelete("DiaryDelete")]
        public async Task<IActionResult>DairyDelete(DairyDelete dairyDelete)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (dairyDelete == null)
            {
                return BadRequest();
            }
            var result = await _dailyDietServices.DairyDelete(dairyDelete, uuid);
            return result;
        }

        [HttpGet("DairyList")]
        public async Task<IActionResult>DairyList([FromQuery] DairyListDto dairyList)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (dairyList == null)
            {
                return BadRequest();
            }
            var result = await _dailyDietServices.DairyList(dairyList, uuid);
            return result;
        }
    }
}
