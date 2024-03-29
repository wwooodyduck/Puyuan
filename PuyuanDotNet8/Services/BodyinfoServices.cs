using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PuyuanDotNet8.Data;
using PuyuanDotNet8.Dtos;
using System;

namespace PuyuanDotNet8.Services
{
    public class BodyinfoServices
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        JsonResult success = new JsonResult(new { status = "0" });
        JsonResult fail = new JsonResult(new { status = "1" });
        public BodyinfoServices(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public async Task<IActionResult> BloodSugar(BloodSugarDto bloodSugar,string uuid)
        {
            BloodSugar _bloodsugar = new BloodSugar()
            {
                Uuid = uuid,
                Sugar = bloodSugar.sugar,
                Timeperiod= bloodSugar.timeperiod
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
        public async Task<IActionResult> HbA1cUpload(HbA1cDto hbA1Cdto,string uuid)
        {
            HbA1c _hbA1C = new HbA1c()
            {
                Uuid = uuid,
                A1c= hbA1Cdto.alc,
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
        public async Task<IActionResult> HbA1cGet(string uuid)
        {
            var user = _context.HbA1c.Where(h => h.Uuid == uuid).ToList();
            if (user==null)
            {
                return fail;
            }
            var response = new
            {
                status = "0",
                a1cs = user.Select(user => new
                {
                    id = user.Id,
                    user_id = user.Uuid, // 重命名 uuid 為 user_id
                    a1c = user.A1c,
                    recorded_At = user.Recorded_At,
                    created_At = user.Created_At,
                    updated_At = user.Updated_At,
                }).ToList() // 將結果轉換為列表
            };
            JsonResult success = new JsonResult(response);
            return success;
        }
        public async Task<IActionResult> HbA1cDelete(HbA1cDelete hbA1Cdelete,string uuid)
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
    }    
}
