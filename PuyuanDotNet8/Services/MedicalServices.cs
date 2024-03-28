using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace PuyuanDotNet8.Services
{
    public class MedicalServices
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        JsonResult success = new JsonResult(new { status = "0" });
        JsonResult fail = new JsonResult(new { status = "1" });
        public MedicalServices(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IActionResult> MedcialGet(string uuid)
        {
            var user = _context.MedicalInformation.FirstOrDefault(h => h.Uuid == uuid);
            if (user == null)
            {
                return fail;
            }
            
            var response = new
            {
                status = "0",
                user = new
                {
                    user = new
                    {
                        id = user.Id,
                        user_id = user.Uuid, // 重命名 uuid 為 user_id
                        diabetes_type = user.Diabetes_Type,
                        oad = user.Oad,
                        insulin = user.Insulin,
                        anti_hypertensive = user.Anti_Hypertensives,
                        createed_at = user.Created_At,
                        updated_at = user.Updated_At,
                    }
                }
            };

            JsonResult success = new JsonResult(response);
            return success;
        }

        public async Task<IActionResult> MedcialUpdate(MedicalDto MedicalDto,string uuid)
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
    }
}
