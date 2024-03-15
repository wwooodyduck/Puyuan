using Microsoft.AspNetCore.Mvc;

namespace PuyuanDotNet8.Services
{
    public class AuthService
    {
        private readonly DataContext _context;
        private readonly PasswordHelper _passwordHelper;
        private readonly JwtHelper _jwthelper;

        JsonResult faila = new JsonResult(new { status = "1" });
        JsonResult failb = new JsonResult(new { status = "3" });
        JsonResult failc = new JsonResult(new { status = "4" });

        public AuthService(DataContext context, PasswordHelper passwordHelper, JwtHelper jwtHelper)
        {
            _context = context;
            _passwordHelper = passwordHelper;
            _jwthelper = jwtHelper;
        }

        public async Task<IActionResult> Login(LoginDto login)
        {
            JsonResult uncheckMail = new JsonResult(new { status = "2" });
            var user = _context.UserProfile
                        .Include(e => e.UserSet)
                        .SingleOrDefault(e => e.Username.Equals(login.Username));
            if (user == null)
            {
                return faila;
            }
            if(!user.UserSet.Verified)
            {
                return failb;
            }
            if(!_passwordHelper.VerifyPassword(login.Password, user.Password))
            {
                return failc;
            }

            var token = _jwthelper.GetJwtToken(user.Uuid, "user", user.Username);
            JsonResult success = new JsonResult(new { status = "0", token });
            return success;
        }
    }
}
