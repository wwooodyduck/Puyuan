using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using PuyuanDotNet8.Data;
using PuyuanDotNet8.Dtos;

namespace PuyuanDotNet8.Services
{
    public class DailyDietServices
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        JsonResult success = new JsonResult(new { status = "0" });
        JsonResult fail= new JsonResult(new { status = "1" });
        public DailyDietServices(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IActionResult> DailyDietuploald(DailyDietDto dailyDietDto,string uuid)
        {
            DiaryDiet _diaryDiet = new DiaryDiet()
            {
                Uuid = uuid,
                Description=dailyDietDto.description,
                Meal=dailyDietDto.meal,
                Tag=dailyDietDto.tag,
                Image=dailyDietDto.image,
                Lat=dailyDietDto.lat,
                Lng=dailyDietDto.lng,
                Recorded_At=dailyDietDto.recorded_at,
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
                image_url = _diaryDiet.Image,
            };
            JsonResult success = new JsonResult(response);
            return success;
        }
        public async Task<IActionResult> DairyDelete(DairyDelete dairyDelete,string uuid)
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
        public async Task<IActionResult> DairyList(DairyListDto dairyList,string uuid)
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
            if(bloodsugarcontent == null)
            {
                return fail;
            }
            if (bloodPressureContent == null)
            {
                return fail;
            }
            if(dairydietcontent == null)
            {
                return fail;
            }
            var respone = new
            {
                status = "0",
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
    }
}
