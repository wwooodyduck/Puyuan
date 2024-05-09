using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PuyuanDotNet8.Data;
using PuyuanDotNet8.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PuyuanDotNet8.Services
{
    public class UsersetService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        JsonResult success = new JsonResult(new { status = "0" ,message="成功"});  
        JsonResult fail = new JsonResult(new { status = "1", message = "失敗" });
        public UsersetService(DataContext context,  IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<IActionResult> UserSet(UsersetDto userset, string uuid)
        {
            var usersets = _context.UserSet.SingleOrDefault(e => e.Uuid.Equals(uuid));
            var userprofile = _context.UserProfile.SingleOrDefault(e => e.Uuid.Equals(uuid));

            if (!string.IsNullOrEmpty(userset.name))
                usersets.Name = userset.name;

            if (!string.IsNullOrEmpty(userset.birthday))
            {
                DateTime birthday;
                if (DateTime.TryParse(userset.birthday, out birthday))
                {
                    usersets.Birthday = birthday;
                }
                else
                {
                    return fail;
                }
            }


            if (!string.IsNullOrEmpty(userset.weight))
                usersets.Weight = int.Parse(userset.weight);

            if (!string.IsNullOrEmpty(userset.phone))
                userprofile.Phone = userset.phone;

            if (!string.IsNullOrEmpty(userset.email))
                userprofile.email = userset.email;


                usersets.Gender = userset.gender;

            if (!string.IsNullOrEmpty(userset.fcm_id))
                usersets.Fcm_Id = userset.fcm_id;

            if (!string.IsNullOrEmpty(userset.address))
                usersets.Address = userset.address;

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

        public async Task<IActionResult> BloodPressureUpload(BodyDto Bloodpressure, string uuid)
        {
            BloodPressure bloodPressure = new BloodPressure()
            {
                Uuid = uuid,
                Systolic = Bloodpressure.systolic,
                Diastolic = Bloodpressure.diastolic,
                Pulse = Bloodpressure.pulse
            };
            bloodPressure.Recorded_At = DateTime.UtcNow;
            _context.BloodPressure.Add(bloodPressure);
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

        public async Task<IActionResult> WeightUpload(WeightDto weightdto, string uuid)
        {
            _Weight _weight = new _Weight()
            {
                Uuid = uuid,
                Weight = weightdto.weight,
                Body_Fat = weightdto.body_fat,
                Bmi = weightdto.bmi
            };

            _context._Weight.Add(_weight);
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

        public async Task<IActionResult> BloodSugar(BloodSugarDto bloodSugar, string uuid)
        {
            BloodSugar _bloodsugar = new BloodSugar()
            {
                Uuid = uuid,
                Sugar = bloodSugar.sugar,
                Timeperiod = bloodSugar.timeperiod
            };
            bloodSugar.recorded_at = DateTime.Now;
            _context.BloodSugar.Add(_bloodsugar);
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

        public async Task<IActionResult> DairyList(DairyListDto dairyList, string uuid)
        {
            if (!DateTime.TryParse(dairyList.date, out var recordedAt))
            {
                return fail;
            }
            var bloodPressureContent = _context.BloodPressure.FirstOrDefault(h => h.Uuid.Equals(uuid) && h.Recorded_At.Date == recordedAt.Date);
            var weightcontent = _context._Weight.FirstOrDefault(h => h.Uuid.Equals(uuid) && h.Recorded_At.Date == recordedAt.Date);
            var bloodsugarcontent = _context.BloodSugar.FirstOrDefault(h => h.Uuid.Equals(uuid) && h.Recorded_At.Date == recordedAt.Date);
            var dairydietcontent = _context.DiaryDiet.FirstOrDefault(h => h.Uuid.Equals(uuid) && h.Recorded_At.Date == recordedAt.Date);
            if (weightcontent == null)
            {
                return fail;
            }
            if (bloodsugarcontent == null)
            {
                return fail;
            }
            if (bloodPressureContent == null)
            {
                return fail;
            }
            if (dairydietcontent == null)
            {
                return fail;
            }
            var respone = new
            {
                status = "0",
                message = "成功",
                diary = new
                {
                    blood_pressure = new
                    {
                        id = bloodPressureContent.Id,
                        user_id = bloodPressureContent.Uuid,
                        systolic = bloodPressureContent.Systolic,
                        diastolic = bloodPressureContent.Diastolic,
                        pulse = bloodPressureContent.Pulse,
                        recorded_at = bloodPressureContent.Recorded_At,
                        type = "blood_pressure"
                    },
                    weight = new
                    {
                        id = weightcontent.Id,
                        user_id = weightcontent.Uuid,
                        weight = weightcontent.Weight,
                        body_fat = weightcontent.Body_Fat,
                        bmi = weightcontent.Bmi,
                        recorded_at = weightcontent.Recorded_At,
                        type = "weight"
                    },
                    bloodsugar = new
                    {
                        id = bloodsugarcontent.Id,
                        user_id = bloodsugarcontent.Uuid,
                        sugar = bloodsugarcontent.Sugar,
                        timeperiod = bloodsugarcontent.Timeperiod,
                        recorded_at = bloodsugarcontent.Recorded_At,
                        type = "blood_sugar"
                    },
                    diet = new
                    {
                        id = dairydietcontent.Id,
                        user_id = dairydietcontent.Uuid,
                        description = dairydietcontent.Description,
                        meal = dairydietcontent.Meal,
                        tag = new
                        {
                            name = dairydietcontent.Tag
                        },
                        image = new
                        {
                            dairydietcontent.Image
                        },
                        location = new
                        {
                            lat = dairydietcontent.Lat,
                            lng = dairydietcontent.Lng,
                        },
                        recorded_at = dairydietcontent.Recorded_At,
                        type = "diet",
                        reply = "安安"
                    }
                }
            };
            JsonResult success = new JsonResult(respone);
            return success;
        }

        public async Task<IActionResult> DailyDietuploald(DailyDietDto dailyDietDto, string uuid)
        {
            DiaryDiet _diaryDiet = new DiaryDiet()
            {
                Uuid = uuid,
                Description = dailyDietDto.description,
                Meal = dailyDietDto.meal,
                Tag = dailyDietDto.tag,
                Image = dailyDietDto.image,
                Lat = dailyDietDto.lat,
                Lng = dailyDietDto.lng,
                Recorded_At = dailyDietDto.recorded_at,
            };
            _context.DiaryDiet.Add(_diaryDiet);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)// 如果存在資料庫更新發生異常，則返回失敗结果
            {
                return fail;
            }

            var response = new
            {
                status = "0",
                message = "成功",
                image_url = _diaryDiet.Image,
            };
            JsonResult success = new JsonResult(response);
            return success;
        }

        public async Task<IActionResult> DairyDelete(DairyDelete dairyDelete, string uuid)
        {
            var bloodSugars = _context.BloodSugar.Where(h => h.Uuid == uuid).OrderBy(h => h.Id).ToList();
            var bloodSugarsindex = dairyDelete.blood_sugar.Distinct().OrderBy(x => x).ToList();
            var bloodsugarrecord = new List<BloodSugar>();
            foreach (var index in bloodSugarsindex)
            {
                if (index >= 0 && index < bloodSugars.Count) // 确保索引有效
                {
                    bloodsugarrecord.Add(bloodSugars[index]);
                }
            }
            var bloodpressure = _context.BloodPressure.Where(h => h.Uuid == uuid).OrderBy(h => h.Id).ToList();
            var bloodpressuresindex = dairyDelete.blood_pressure.Distinct().OrderBy(x => x).ToList();
            var bloodpressurerecord = new List<BloodPressure>();
            foreach (var index in bloodpressuresindex)
            {
                if (index >= 0 && index < bloodpressure.Count) // 确保索引有效
                {
                    bloodpressurerecord.Add(bloodpressure[index]);
                }
            }
            var diaryDiets = _context.DiaryDiet.Where(h => h.Uuid == uuid).OrderBy(h => h.Id).ToList();
            var dietsindex = dairyDelete.diary_diets.Distinct().OrderBy(x => x).ToList();
            var DiaryDietrecord = new List<DiaryDiet>();
            foreach (var index in dietsindex)
            {
                if (index >= 0 && index < diaryDiets.Count) // 确保索引有效
                {
                    DiaryDietrecord.Add(diaryDiets[index]);
                }
            }
            var weights = _context._Weight.Where(h => h.Uuid == uuid).OrderBy(h => h.Id).ToList();
            var weightsindex = dairyDelete.weights.Distinct().OrderBy(x => x).ToList();
            var weightrecord = new List<_Weight>();
            foreach (var index in weightsindex)
            {
                if (index >= 0 && index < weights.Count) // 确保索引有效
                {
                    weightrecord.Add(weights[index]);
                }
            }
            _context.BloodSugar.RemoveRange(bloodsugarrecord);
            _context.BloodPressure.RemoveRange(bloodpressurerecord);
            _context.DiaryDiet.RemoveRange(DiaryDietrecord);
            _context._Weight.RemoveRange(weightrecord);
            await _context.SaveChangesAsync();
            return success;
        }

        public async Task<IActionResult> HbA1cGet(string uuid)
        {
            //var user = _context.HbA1c.Where(h => h.Uuid == uuid).ToList();
            var user = _context.HbA1c.Where(h => h.Uuid == uuid).ToList();
            
            var userprofile = _context.UserProfile.SingleOrDefault(h => h.Uuid == uuid);
            if (user == null)
            {
                HbA1c @default = new HbA1c()
                {
                    Uuid = uuid,
                    Created_At = DateTime.Now,
                };
            }
            var response = new
            {
                status = "0",
                message = "成功",
                a1cs = user.Select(user => new
                {
                    id = user.Id,
                    user_id = userprofile.Id,
                    a1c = user.A1c,
                    recorded_At = user.Recorded_At,
                    created_At = user.Created_At,
                    updated_At = user.Updated_At,
                })
            };
            JsonResult success = new JsonResult(response);
            return success;
        }

        public async Task<IActionResult> HbA1cUpload(HbA1cDto hbA1Cdto, string uuid)
        {
            HbA1c _hbA1C = new HbA1c()
            {
                Uuid = uuid,
                A1c = hbA1Cdto.alc,
            };
            _context.HbA1c.Add(_hbA1C);
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

        public async Task<IActionResult> HbA1cDelete(HbA1cDelete hbA1Cdelete, string uuid)
        {
            var matchedRecords = _context.HbA1c.Where(h => h.Uuid == uuid).OrderBy(h => h.Id).ToList(); // 假设按 Id 排序
            var indexesToDelete = hbA1Cdelete.ids.Distinct().OrderBy(x => x).ToList(); // 去重并排序
            var recordsToDelete = new List<HbA1c>(); // 用你的实体类型替换 YourEntityType
            foreach (var index in indexesToDelete)
            {
                if (index >= 0 && index < matchedRecords.Count) // 确保索引有效
                {
                    recordsToDelete.Add(matchedRecords[index]);
                }
            }
            if (!recordsToDelete.Any())
            {
                return fail;
            }
            // 删除记录
            _context.HbA1c.RemoveRange(recordsToDelete);
            // 保存更改
            await _context.SaveChangesAsync();
            return success;
        }

        public async Task<IActionResult> MedcialGet(string uuid)
        {
            var user = _context.MedicalInformation.FirstOrDefault(h => h.Uuid == uuid);
            var userprofile=_context.UserProfile.FirstOrDefault(h => h.Uuid == uuid);
            if (user == null)
            {
                return fail;
            }
            var response = new
            {
                status = "0",
                message = "成功", 
                medical_info = new
                {
                    id = user.Id,
                    user_id = userprofile.Id,
                    diabetes_type = user.Diabetes_Type,
                    oad = user.Oad ? 1:0,
                    insulin = user.Insulin ? 1 : 0,
                    anti_hypertensives = user.Anti_Hypertensives ? 1 : 0,
                    created_at = user.Created_At,
                    updated_at = user.Updated_At,
                }
            };
            JsonResult success = new JsonResult(response);
            return success;
        }

        public async Task<IActionResult> MedcialUpdate(MedicalDto MedicalDto, string uuid)
        {
            var user = _context.MedicalInformation.FirstOrDefault(h => h.Uuid == uuid);
            if (user == null)
            {
                return fail;
            }
            user = _mapper.Map(MedicalDto, user);
            user.Updated_At = DateTime.Now;
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

        public async Task<IActionResult> Druginfoget(DrugDto drugget, string uuid)
        {
            var user = _context.DrugInformation.Where(h => h.Uuid == uuid && h.Drug_Type == drugget.type);
            if (user == null)
            {
                return fail;
            }
            var response = new
            {
                status = "0",
                message = "成功",
                a1cs = user.Select(user => new
                {
                    id = user.Id,
                    user_id = user.Uuid,
                    type = user.Drug_Type,
                    name = user.Name,
                    recorded_at = user.Recorded_At
                })
            };
            JsonResult success = new JsonResult(response);
            return success;
        }

        public async Task<IActionResult> DruginfoUpload(DrugUploadDto drugUpload, string uuid)
        {
            var user = _context.DrugInformation.FirstOrDefault(h => h.Uuid == uuid);
            DrugInformation _druginfo = new DrugInformation()
            {
                Uuid = uuid,
                Drug_Type = drugUpload.Type,
                Name = drugUpload.Name,
                Recorded_At = drugUpload.recorded_at
            };
            _druginfo.Created_At = DateTime.Now;
            _druginfo.Updated_At = DateTime.Now;
            _context.DrugInformation.Add(_druginfo);
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
        public async Task<IActionResult> DrugInfoDelete(DrugDeleteDto drugDelete, string uuid)
        {
            var matchedRecords = _context.DrugInformation.Where(h => h.Uuid == uuid).OrderBy(h => h.Id).ToList(); // 假设按 Id 排序
            var indexesToDelete = drugDelete.ids.Distinct().OrderBy(x => x).ToList(); // 去重并排序
            var recordsToDelete = new List<DrugInformation>(); // 用你的实体类型替换 YourEntityType
            foreach (var index in indexesToDelete)
            {
                if (index >= 0 && index < matchedRecords.Count) // 确保索引有效
                {
                    recordsToDelete.Add(matchedRecords[index]);
                }
            }
            if (!recordsToDelete.Any())
            {
                return fail;
            }
            _context.DrugInformation.RemoveRange(recordsToDelete);
            await _context.SaveChangesAsync();
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
                message = "成功",
                blood_pressure = _bloodpressure.Recorded_At,
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
                message = "成功",
                bloodsugar =new
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
                status = "0",
                message = "成功",
                user = new
                {
                    id = userprofilecontent.Id,
                    name = usersetcontent.Name,
                    account = userprofilecontent.Username,
                    email = userprofilecontent.email,
                    phone = userprofilecontent.Phone,
                    fb_id = userprofilecontent.Fb_Id,
                    status = usersetcontent.Status,
                    group = usersetcontent.Group,
                    birthday = usersetcontent.Birthday,
                    height = usersetcontent.Height,
                    weight = usersetcontent.Weight,
                    gender = usersetcontent.Gender ? 1 : 0,
                    address = usersetcontent.Address,
                    unread_records = new List<int>
                    {
                        usersetcontent.UnreadRecordsOne,
                        usersetcontent.UnreadRecordsTwo,
                        usersetcontent.UnreadRecordsThree,
                    },
                    verified = usersetcontent.Verified ? 1 : 0,
                    privacy_policy = usersetcontent.Privacy_Policy ? 1 : 0,
                    must_change_password = usersetcontent.Must_Change_Password ? 1 : 0,
                    fcm_id = usersetcontent.Fcm_Id,
                    login_times = usersetcontent.login_times,
                    created_at = usersetcontent.Created_At,
                    updated_at = usersetcontent.Updated_At,
                    @default = new
                    {
                        id = userdefaultcontent.Id,
                        user_id = userprofilecontent.Id,
                        sugar_delta_max = userdefaultcontent.Sugar_Delta_Max,
                        sugar_delta_min = userdefaultcontent.Sugar_Delta_Min,
                        sugar_morning_max = userdefaultcontent.Sugar_Morning_Max,
                        sugar_morning_min = userdefaultcontent.Sugar_Morning_Min,
                        sugar_evening_max = userdefaultcontent.Sugar_Evening_Max,
                        sugar_evening_min = userdefaultcontent.Sugar_Evening_Min,
                        sugar_before_max = userdefaultcontent.Sugar_Before_Max,
                        sugar_before_min = userdefaultcontent.Sugar_Before_Min,
                        sugar_after_max = userdefaultcontent.Sugar_After_Max,
                        sugar_after_min = userdefaultcontent.Sugar_After_Min,
                        systolic_max = userdefaultcontent.Systolic_Max,
                        systolic_min = userdefaultcontent.Systolic_Min,
                        diastolic_max = userdefaultcontent.Diastolic_Max,
                        diastolic_min = userdefaultcontent.Diastolic_Min,
                        pulse_max = userdefaultcontent.Pulse_Max,
                        pulse_min = userdefaultcontent.Pulse_Min,
                        weight_max = userdefaultcontent.Weight_Max,
                        weight_min = userdefaultcontent.Weight_Min,
                        bmi_max = userdefaultcontent.Bmi_Max,
                        bmi_min = userdefaultcontent.Bmi_Min,
                        body_fat_max = userdefaultcontent.Body_Fat_Max,
                        body_fat_min = userdefaultcontent.Body_Fat_Min,
                        created_at = userdefaultcontent.Created_At,
                        updated_at = userdefaultcontent.Updated_At,
                    },
                    setting = new
                    {
                        id = settingcontent.Id,
                        user_id = userprofilecontent.Id,
                        after_recording = settingcontent.After_Recording ? 1 : 0,
                        no_recording_for_a_day = settingcontent.No_Recording_For_A_Day ? 1 : 0,
                        over_max_or_under_min = settingcontent.Over_Max_Or_Under_Min ? 1 : 0,
                        after_meal = settingcontent.After_Meal ? 1 : 0,
                        unit_of_sugar = settingcontent.Unit_Of_Sugar ? 1 : 0,
                        unit_of_weight = settingcontent.Unit_Of_Weight ? 1 : 0,
                        unit_of_height = settingcontent.Unit_Of_Height ? 1 : 0,
                        created_at = settingcontent.Created_At,
                        updated_at = settingcontent.Updated_At,
                    },
                    vip = new
                    {
                        id = 1,
                        user_id = 1,
                        level = 0,
                        remark = 0.0,
                        started_at = "2023-02-03 08:17:17",
                        ended_at = "2023-02-03 08:17:17",
                        created_at = "2023-02-03 08:17:17",
                        updated_at = "2023-02-03 08:17:17"
                    }
                }
            };
            var successes= new JsonResult(response);
            return successes;
        }
    }
}
