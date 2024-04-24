using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuyuanDotNet8.Data;
namespace PuyuanDotNet8.Services
{
    public class VerificationService
    {
        private readonly DataContext _datacontext;
        private readonly EmailSenderHelper _emailSender;
        JsonResult success = new JsonResult(new { status = "0", message = "成功" });
        JsonResult fail = new JsonResult(new { status = "1", message = "失敗" });
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
                .SingleOrDefault(e => e.email.Equals(sendVerification.email));
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
                sendVerification.email,
                "普元驗證訊息",
                $"Verification Code: {verifCode}");
            _emailSender.SendEmail(message);
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
                .SingleOrDefault(e => e.email.Equals(checkVerification.email));
            if (user == null)
            {
                return fail;
            }
            var verfi = _datacontext.Verifications.SingleOrDefault(e => e.Uuid.Equals(user.Uuid));
            if (!user.UserSet.Verified && verfi.VerifictionCode.Equals(checkVerification.code))
            {
                user.UserSet.Verified = true;
                _datacontext.Update(user);
                _datacontext.Remove(verfi);
            }
            await _datacontext.SaveChangesAsync();
            /*try
            {
                await _datacontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return fail;
            }*/
            return success;
        }
    }
}
