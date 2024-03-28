using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PuyuanDotNet8.Data;
using PuyuanDotNet8.Helpers;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> Userinfo(string uuid)
        {
            var usersetcontent = _context.UserSet.SingleOrDefault(h => h.Uuid.Equals(uuid));
            var userprofilecontent=_context.UserProfile.SingleOrDefault(h=>h.Uuid.Equals(uuid));
            var userdefaultcontent = _context.Default.SingleOrDefault(h => h.Uuid.Equals(uuid));
            var settingcontent = _context.Setting.SingleOrDefault(h => h.Uuid.Equals(uuid));

            var response = new
            {
                status = 0,
                user = new
                {
                    id=usersetcontent.Uuid,
                    name=usersetcontent.Name,
                    account= userprofilecontent.Username,
                    email=userprofilecontent.Email,
                    phone=userprofilecontent.Phone,
                    fb_id=userprofilecontent.Fb_Id,
                    status=usersetcontent.Status,
                    group=usersetcontent.Group,
                    birthday=usersetcontent.Birthday,
                    height=usersetcontent.Height,
                    weight=usersetcontent.Weight,
                    gender=usersetcontent.Gender,
                    address=usersetcontent.Address,
                    unread_records = new
                    {
                        unread_record= usersetcontent.UnreadRecordsOne,
                        unread_recordo= usersetcontent.UnreadRecordsTwo,
                        unread_recordt= usersetcontent.UnreadRecordsThree,
                    },
                    verified=usersetcontent.Verified,
                    privacy_policy=usersetcontent.Privacy_Policy,
                    must_change_password= usersetcontent.Must_Change_Password,
                    fcm_id=usersetcontent.Fcm_Id,
                    badge=usersetcontent.Badge,
                    login_time=usersetcontent.Login_Times,
                    created_at=usersetcontent.Created_At,
                    updated_at=usersetcontent.Updated_At,
                },
                defaults = new
                {
                    id=userdefaultcontent.Id,
                    user_id=userdefaultcontent.Uuid,
                    sugar_delta_max=userdefaultcontent.Suger_Delta_Max,
                    sugar_delta_min=userdefaultcontent.Suger_Delta_Min,
                    sugar_morning_max=userdefaultcontent.Suger_Morning_Max,
                    sugar_morning_min= userdefaultcontent.Suger_Morning_Min,
                    sugar_evening_max=userdefaultcontent.Suger_Evening_Max,
                    sugar_evening_min=userdefaultcontent.Suger_Evening_Min,
                    sugar_before_max=userdefaultcontent.Suger_Before_Max,
                    sugar_before_min=userdefaultcontent.Suger_Before_Min,
                    sugar_after_max=userdefaultcontent.Suger_After_Max,
                    sugar_after_min=userdefaultcontent.Suger_After_Min,
                    systolic_max=userdefaultcontent.Systolic_Max,
                    systolic_min=userdefaultcontent.Systolic_Min,
                    diastolic_max=userdefaultcontent.Diastolic_Max,
                    diastolic_min=userdefaultcontent.Diastolic_Min,
                    pulse_max=userdefaultcontent.Pulse_Max,
                    pulse_min=userdefaultcontent.Pulse_Min,
                    weight_max=userdefaultcontent.Weight_Max,
                    weight_min=userdefaultcontent.Weight_Min,
                    bmi_max=userdefaultcontent.Bmi_Max,
                    bmi_min=userdefaultcontent.Bmi_Min,
                    body_fat_max=userdefaultcontent.Body_Fat_Max,
                    body_fat_min=userdefaultcontent.Body_Fat_Min,
                    created_at=userdefaultcontent.Created_At,
                    updated_at=userdefaultcontent.Updated_At,
                },
                setting = new
                {
                    id=settingcontent.Id,
                    user_id=settingcontent.Uuid,
                    after_recording=settingcontent.After_Recording,
                    no_recording_for_a_day=settingcontent.No_Recording_For_A_Day,
                    over_max_or_under_min=settingcontent.Over_Max_Or_Under_Min,
                    after_meals=settingcontent.After_Meal,
                    unit_of_sugar=settingcontent.Unit_Of_Sugar,
                    unit_of_weight=settingcontent.Unit_Of_Weight,
                    unit_of_height=settingcontent.Unit_Of_Height,
                    created_at=settingcontent.Created_At,
                    updated_at=settingcontent.Updated_At,
                }
            };
            var successes= new JsonResult(response);
            return successes;
        }
    }
}
