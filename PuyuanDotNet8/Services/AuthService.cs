﻿using Microsoft.AspNetCore.Mvc;

namespace PuyuanDotNet8.Services
{
    public class AuthService
    {
        private readonly DataContext _context;
        private readonly PasswordHelper _passwordHelper;
        private readonly JwtHelper _jwthelper;
        JsonResult fail = new JsonResult(new { status = "1" ,message="失敗"});
        public AuthService(DataContext context, PasswordHelper passwordHelper, JwtHelper jwtHelper)
        {
            _context = context;
            _passwordHelper = passwordHelper;
            _jwthelper = jwtHelper;
        }
        public async Task<IActionResult> Login(LoginDto login)
        {
            JsonResult uncheckMail = new JsonResult(new { status = "2",message= "信箱未驗證" });
            var user = _context.UserProfile
                        .Include(e => e.UserSet)
                        .SingleOrDefault(e => e.email.Equals(login.email));
            if (user == null)
            {
                return fail;
            }   
            if(!user.UserSet.Verified)
            {
                return uncheckMail;
            }
            if(!_passwordHelper.VerifyPassword(login.password, user.password))
            {
                return fail;
            }
            var token = _jwthelper.GetJwtToken(user.Uuid, "user");
            JsonResult success = new JsonResult(new { status = "0",token, message = "成功" });
            return success;
        }
    }
}
