using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuyuanDotNet8.Data;
namespace PuyuanDotNet8.Services
{
    public class VerificationService
    {
        private readonly DataContext _datacontext;
        private readonly EmailSenderHelper _emailSender;

        JsonResult success = new JsonResult(new { status = "0" });
        JsonResult fail = new JsonResult(new { status = "1" });

        public VerificationService(
            EmailSenderHelper emailSenderHelper,
            DataContext context)
        {
            _datacontext = context;
            _emailSender = emailSenderHelper;
        }
        public async Task<IActionResult> SendVerification(SendVerificationDto sendVerification)
        {
            var user = _datacontext.UserProfile
                .Include(e => e.UserSet)
                .SingleOrDefault(e => e.Email.Equals(sendVerification.Email) && e.Phone.Equals(sendVerification.Phone));
            if (user == null)
            {
                return fail;
            }

            if (user.UserSet.Verified.Equals(true))
            {
                return fail;
            }

            var verif = _datacontext.Verifications.SingleOrDefault(e => e.Uuid.Equals(user.Uuid));
            var verifCode = RandomCodeHelper.Create(32);
            if (verif == null)
            {
                Verification verification = new Verification()
                {
                    Uuid = user.Uuid,
                    VerifictionCode = verifCode,
                };
                _datacontext.Verifications.Add(verification);
            }
            else
            {
                verif.VerifictionCode = verifCode;
                _datacontext.Verifications.Update(verif);
            }

            var message = new MessageDto(
                sendVerification.Email,
                "普元驗證訊息",
                $"Verification Code: {verifCode}");
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
                await _datacontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return fail;
            }

            return success;
        }
        public async Task<IActionResult> CheckVerification(CheckVerificationDto checkVerification)
        {
            var user = _datacontext.UserProfile
                .Include(e => e.UserSet)
                .SingleOrDefault(e => e.Phone.Equals(checkVerification.Phone));
            if (user == null)
            {
                return fail;
            }

            var verfi = _datacontext.Verifications.SingleOrDefault(e => e.Uuid.Equals(user.Uuid));
            if (!user.UserSet.Verified && verfi.VerifictionCode.Equals(checkVerification.VerifictionCode))
            {
                user.UserSet.Verified = true;
                _datacontext.Update(user);
                _datacontext.Remove(verfi);
            }

            try
            {
                await _datacontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return fail;
            }
            return success;
        }
    }
}
