using Microsoft.AspNetCore.Mvc;

namespace PuyuanDotNet8.Services
{
    public class ForgetPasswordService
    {
        private readonly DataContext _context;
        private readonly EmailSenderHelper _emailSender;
        private readonly JwtHelper _jwthelper;
        private readonly RandomCodeHelper _randomCodeHelper;

        JsonResult success = new JsonResult(new { status = "0" });
        JsonResult fail = new JsonResult(new { status = "1" });


        public ForgetPasswordService(DataContext context, EmailSenderHelper emailService, JwtHelper jwtHelper, RandomCodeHelper randomCodeHelper)
        {
            _context = context;
            _emailSender = emailService;
            _jwthelper = jwtHelper;
            _randomCodeHelper = randomCodeHelper;
        }

        public async Task<IActionResult> ForgotPassword(SendVerificationDto forgets)
        {
            var newPassword = RandomCodeHelper.Create(10);

            var user = _context.UserProfile
                .Include(e => e.UserSet)
                .SingleOrDefault(e => e.Email.Equals(forgets.Email) && e.Phone.Equals(forgets.Phone));
            if (user == null)
            {
                return fail;
            }
            
           
            user.Password = newPassword;
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

        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {

        }
    }
}
