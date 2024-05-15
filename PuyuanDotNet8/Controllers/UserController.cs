using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PuyuanDotNet8.Data;
using PuyuanDotNet8.Dtos;
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

        [HttpPatch("default")]
        public async Task<IActionResult>UserDefault(UserDefaultDto userDefault)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if(userDefault==null)
            {
                return BadRequest();
            }                                                               
            var result= await _usersetService.UserDefault(userDefault,uuid);
            return result;
        }

        [HttpPatch("setting")]
        public async Task<IActionResult> Setting(SettingDto setting)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (setting == null)
            {
                return BadRequest();
            }
            var result = await _usersetService.Setting(setting,uuid);
            return result;
        }

        [HttpPost("blood/pressure")]
        public async Task<IActionResult> BloodPressureUpload(BodyDto Bloodpressure)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (Bloodpressure == null)
            {
                return BadRequest();
            }
            var result = await _usersetService.BloodPressureUpload(Bloodpressure, uuid);
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
            var result = await _usersetService.WeightUpload(Weightdto, uuid);
            return result;
        }

        [HttpPost("blood/sugar")]
        public async Task<IActionResult> BloodSugar(BloodSugarDto bloodSugar)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (bloodSugar == null)
            {
                return BadRequest();
            }
            var result = await _usersetService.BloodSugar(bloodSugar, uuid);
            return result;
        }

        [HttpGet("last-update")]
        public async Task<IActionResult> LastUpdate()
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _usersetService.LastUpdate(uuid);
            return result;
        }

        [HttpPost("records")]
        public async Task<IActionResult> lastrecorded(LastRecordDto lastRecord)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (lastRecord == null)
            {
                return BadRequest("bad");
            }
            var result = await _usersetService.lastrecorded(lastRecord, uuid);
            return result;
        }

        [HttpGet("dairy")]
        public async Task<IActionResult> DairyList([FromQuery] DairyListDto dairyList)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (dairyList == null)
            {
                return BadRequest();
            }
            var result = await _usersetService.DairyList(dairyList, uuid);
            return result;
        }

        [HttpPost("diet")]
        public async Task<IActionResult> DailyDietuploald(DailyDietDto dailyDietDto)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (dailyDietDto == null)
            {
                return BadRequest();
            }
            var result = await _usersetService.DailyDietuploald(dailyDietDto, uuid);
            return result;
        }

        [HttpDelete("records")]
        public async Task<IActionResult> DairyDelete(DairyDelete dairyDelete)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (dairyDelete == null)
            {
                return BadRequest();
            }
            var result = await _usersetService.DairyDelete(dairyDelete, uuid);
            return result;
        }
        [Authorize]
        [HttpGet("a1c")]
        public async Task<IActionResult> HbA1cGet()
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _usersetService.HbA1cGet(uuid);
            
            return result;
        }

        [HttpPost("alc")]
        public async Task<IActionResult> HbA1cUpload(HbA1cDto hbA1Cdto)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (hbA1Cdto == null)
            {
                return BadRequest();
            }
            var result = await _usersetService.HbA1cUpload(hbA1Cdto, uuid);
            return result;
        }

        [HttpDelete("alcs")]
        public async Task<IActionResult> HbA1cDelete(HbA1cDelete hbA1Cdelete)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _usersetService.HbA1cDelete(hbA1Cdelete, uuid);
            return result;
        }

        [HttpGet("medical")]
        public async Task<IActionResult> MedcialGet()
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _usersetService.MedcialGet(uuid);
            return result;
        }

        [HttpPatch("medical")]
        public async Task<IActionResult> MedcialUpdate(MedicalDto MedicalDto)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;

            if (MedicalDto == null)
            {
                return BadRequest("bad");
            }

            var result = await _usersetService.MedcialUpdate(MedicalDto, uuid);
            return result;
        }

        [HttpGet("drug-used")]
        public async Task<IActionResult> Druginfoget([FromQuery] DrugDto drugget)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;

            if (drugget == null)
            {
                return BadRequest("bad");
            }

            var result = await _usersetService.Druginfoget(drugget, uuid);
            return result;
        }

        [HttpPost("drug-used")]
        public async Task<IActionResult> DruginfoUpload(DrugUploadDto drugUpload)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (drugUpload == null)
            {
                return BadRequest("bad");
            }

            var result = await _usersetService.DruginfoUpload(drugUpload, uuid);
            return result;
        }

        [HttpDelete("drug-used")]
        public async Task<IActionResult> DrugInfoDelete(DrugDeleteDto drugDelete)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _usersetService.DrugInfoDelete(drugDelete, uuid);
            return result;
        }

        [HttpPut("badge")]
        public async Task<IActionResult> BadgeUpdate(BadgeUpdateDto badgeUpdate)
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            if (badgeUpdate == null)
            {
                return BadRequest();
            }
            var result = await _usersetService.BadgeUpdate(badgeUpdate, uuid);
            return result;
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Userinfo()
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _usersetService.Userinfo(uuid);
            return result;
        }
        [HttpGet("care")]
        public async Task<IActionResult> careget()
        {
            var uuid = User.Claims.First(claim => claim.Type == "jti").Value;
            var result = await _usersetService.careget(uuid);
            return result;
        }
       
    }
}
