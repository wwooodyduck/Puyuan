using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using PuyuanDotNet8.Helpers;

namespace PuyuanDotNet8.Services
{
    public class ForgetPasswordService
    {
        private readonly DataContext _context;
        private readonly EmailSenderHelper _emailSender;
        private readonly JwtHelper _jwthelper;
        private readonly RandomCodeHelper _randomCodeHelper;
        private readonly PasswordHelper _passwordHelper;
        JsonResult success = new JsonResult(new { status = "0" });
        JsonResult fail = new JsonResult(new { status = "1" });
        public ForgetPasswordService(DataContext context, EmailSenderHelper emailService, JwtHelper jwtHelper, RandomCodeHelper randomCodeHelper, PasswordHelper passwordHelper)
        {
            _context = context;
            _emailSender = emailService;
            _jwthelper = jwtHelper;
            _randomCodeHelper = randomCodeHelper;
            _passwordHelper = passwordHelper;
        }
        public async Task<IActionResult> ForgotPassword(SendVerificationDto forgets)
        {
            var newPassword = RandomCodeHelper.Create(10);
            var hashpassword= _passwordHelper.HashPassword(newPassword);
            var user = _context.UserProfile
                .Include(e => e.UserSet)
                .SingleOrDefault(e => e.Email.Equals(forgets.Email));
            if (user == null)
            {
                return fail;
            }
            user.Password = hashpassword;
            _context.SaveChanges();
            var message = new MessageDto(
                forgets.Email,
                "普元忘記密碼",
                $"Verification Code: {newPassword}");
            try
            {
                _emailSender.SendEmail(message);
            }
            catch
            {
                return fail;
            }
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
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword, string uuid)
        {
            var user = _context.UserProfile.Include(e => e.UserSet).SingleOrDefault(e => e.Uuid.Equals(uuid));
            if (user == null)
            {
                return fail;
            }
            var pw = _passwordHelper.HashPassword(resetPassword.Password);
            user.Password = pw;
            _context.SaveChangesAsync();
            return success;
        }
    }
}
