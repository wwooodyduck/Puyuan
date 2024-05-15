using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace PuyuanDotNet8.Services
{
    public class shareServices
    {
        private readonly DataContext _context;
        JsonResult success = new JsonResult(new { status = "0" });
        JsonResult fail = new JsonResult(new { status = "1" });
        public shareServices(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult>shareget(string uuid)
        {
            var userprofile=_context.UserProfile.SingleOrDefault();
            var userset=_context.UserSet.SingleOrDefault();
            var userpressure=_context.BloodPressure.SingleOrDefault();
            var usersugar=_context.BloodSugar.SingleOrDefault();
            var userweight=_context._Weight.SingleOrDefault();
            var userdiary=_context.DiaryDiet.SingleOrDefault();
            var records= new List<string>();
            JsonResult result = new JsonResult (new {status="0",message="success", records});
            return result;
        }
    }
}
