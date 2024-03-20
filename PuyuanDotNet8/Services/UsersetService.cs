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
        

    }
}
