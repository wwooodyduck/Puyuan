using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PuyuanDotNet8.Data;
using PuyuanDotNet8.Helpers;

namespace PuyuanDotNet8.Services
{
    public class UsersetService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        JsonResult success = new JsonResult(new { status = "0" });  
        JsonResult fail = new JsonResult(new { status = "1" });
        public UsersetService(DataContext context,  IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> UserSet(UsersetDto userset,string uuid)
        {
            var usersets = _context.UserSet.SingleOrDefault(e => e.Uuid.Equals(uuid));
            var userprofile = _context.UserProfile.SingleOrDefault(e => e.Uuid.Equals(uuid));
            usersets = _mapper.Map(userset,usersets);
            userprofile=_mapper.Map(userset, userprofile);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)// 如果存在資料庫更新發生異常，則返回失敗结果
            {
                return fail;
            }
            return success;
        }

        public async Task<IActionResult> UserDefault(UserDefaultDto userDefault,string uuid)
        {
            var userdefault = _context.Default.SingleOrDefault(e => e.Uuid.Equals(uuid));
            userdefault=_mapper.Map(userDefault, userdefault);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return fail;
            }
            return success;
        }

        public async Task<IActionResult> Setting(SettingDto setting,string uuid)
        {
            var usersetting = _context.Setting.SingleOrDefault(e => e.Uuid.Equals(uuid));
            usersetting = _mapper.Map(setting, usersetting);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return fail;
            }
            return success;
        }
        public async Task<IActionResult> BadgeUpdate(BadgeUpdateDto badgeUpdate,string uuid)
        {
            var user = _context.UserSet.SingleOrDefault(e => e.Uuid.Equals(uuid));
             user.Badge= badgeUpdate.badge;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return fail;
            }
            return success;
        }
        public async Task<IActionResult> LastUpdate(string uuid )
        {
            var _bloodpressure = _context.BloodPressure
                   .Where(e => e.Uuid.Equals(uuid))
                   .OrderByDescending(e => e.Recorded_At) // 假设CreatedAt是一个时间戳字段
                   .FirstOrDefault();
            var _weight = _context._Weight
                    .Where(e => e.Uuid.Equals(uuid))
                   .OrderByDescending(e => e.Recorded_At) // 假设CreatedAt是一个时间戳字段
                   .FirstOrDefault();

            var _bloodsugar = _context.BloodSugar
                    .Where(e => e.Uuid.Equals(uuid))
                   .OrderByDescending(e => e.Recorded_At) // 假设CreatedAt是一个时间戳字段
                   .FirstOrDefault();
            var _diet = _context.DiaryDiet
                    .Where(e => e.Uuid.Equals(uuid))
                   .OrderByDescending(e => e.Recorded_At) // 假设CreatedAt是一个时间戳字段
                   .FirstOrDefault();
            var response = new
            {
                status = "0",
                blood_pressure= _bloodpressure.Recorded_At,
                weight=_weight.Recorded_At,
                blood_sugar= _bloodsugar.Recorded_At,
                diet= _diet.Recorded_At
            };

            JsonResult success = new JsonResult(response);
            return success;
        }
        public async Task<IActionResult> lastrecorded(LastRecordDto lastRecord,string uuid)
        {
            var bloodsugarRecord = await _context.BloodSugar
                                      .Where(e => lastRecord.diets.Contains(e.Timeperiod))
                                      .FirstOrDefaultAsync();

            var matchedbloodPressure = _context.BloodPressure.Where(h => h.Uuid == uuid).OrderByDescending(e => e.Recorded_At).FirstOrDefault();
            var matchedweights=_context._Weight.Where(h=>h.Uuid==uuid).OrderByDescending(e => e.Recorded_At).FirstOrDefault();
            var response = new
            {
                status = "0",
                bloodsugar=new
                {
                    id = bloodsugarRecord.Id,
                    user_id = bloodsugarRecord.Uuid,
                    sugar = bloodsugarRecord.Sugar,
                    timeperiod = bloodsugarRecord.Timeperiod,
                    recorded_at = bloodsugarRecord.Recorded_At,
                },
                bloodpressure = new
                {
                    id = matchedbloodPressure.Id,
                    user_id = matchedbloodPressure.Uuid,
                    systolic = matchedbloodPressure.Systolic,
                    diastolic = matchedbloodPressure.Diastolic,
                    pulse = matchedbloodPressure.Pulse,
                    recorded_at = matchedbloodPressure.Recorded_At,
                },
                weight = new
                {
                    id = matchedweights.Id,
                    user_id = matchedweights.Uuid,
                    weight = matchedweights.Weight,
                    body_fat = matchedweights.Body_Fat,
                    bmi = matchedweights.Bmi,
                    recorded_at = matchedweights.Recorded_At
                }
            };
            var successes=new JsonResult(response);
            return successes;
        }
    }
}
