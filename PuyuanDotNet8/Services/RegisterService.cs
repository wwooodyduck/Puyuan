using Microsoft.AspNetCore.Mvc;
using AutoMapper;

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
            var user = _context.UserProfile.SingleOrDefault(e => e.Username == register.Username);
            if (user != null)
            {
                return fail;
            }

            UserProfile userProfile = new UserProfile()
            {
                Uuid = Guid.NewGuid().ToString(),
                Password = _passwordHelper.HashPassword(register.Password)
            };
            userProfile = _mapper.Map(register, userProfile);
            _context.UserProfile.Add(userProfile);

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

            _context.UserSet.Add(userSet);
            _context.Default.Add(@default);
            _context.Setting.Add(setting);
            _context.MedicalInformation.Add(medical);

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
