using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Win32;

namespace PuyuanDotNet8.Services
{
    public class RegisterService
    {
        private readonly DataContext _context;
        private readonly PasswordHelper _passwordHelper;
        private readonly IMapper _mapper;

        JsonResult success = new JsonResult(new { status = "0" });
        JsonResult fail = new JsonResult(new { status = "1" });
        
        
        public RegisterService(DataContext context,PasswordHelper passwordHelper, IMapper mapper)
        {
            _context = context;
            _passwordHelper = passwordHelper;
            _mapper = mapper;
        }

        public async Task<IActionResult> Register(RegisterDto register)
        {

            var user = _context.UserProfile.SingleOrDefault(e => e.Username == register.Username);//檢查資料庫有無相同的Username
            if (user != null)
            {
                return fail;
            }

            // 創建一個新的UserProfile
            UserProfile userProfile = new UserProfile()
            {
                Uuid = Guid.NewGuid().ToString(),// 生成一個新的UUID
                Password = _passwordHelper.HashPassword(register.Password)
            };
            userProfile = _mapper.Map(register, userProfile);// 將RegisterDto的屬性映射到UserProfile上
            _context.UserProfile.Add(userProfile);// 將新的UserProfile添加到資料庫上下文中

            // 為新用戶創建相關數據（UserSet, Default, Setting, MedicalInformation）
            UserSet userSet = new UserSet()
            {
                Uuid = userProfile.Uuid,
                Invite_Code = RandomCodeHelper.Create(10),
                Created_At = userProfile.Created_At
            };

            Default @default = new Default()
            {
                Uuid = userProfile.Uuid,
                Created_At = userProfile.Created_At,
            };

            Setting setting = new Setting()
            {
                Uuid = userProfile.Uuid,
                Created_At = userProfile.Created_At,
            };

            MedicalInformation medical = new MedicalInformation()
            {
                Uuid = userProfile.Uuid,
                Created_At = userProfile.Created_At
            };

            // 將相關數據添加到資料庫上下文中
            _context.UserSet.Add(userSet);
            _context.Default.Add(@default);
            _context.Setting.Add(setting);
            _context.MedicalInformation.Add(medical);

            //嘗試保存更改到資料庫
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
        public async Task<IActionResult> ResgisterComfirm(RegisterComfirmDto registerComfirm)
        {
            var user = _context.UserProfile.SingleOrDefault(e => e.Username == registerComfirm.Account);

            if (user == null)
            {
                return fail;
            }
            else
            {
                return success;
            }
        }
        
    }
}
