using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace PuyuanDotNet8.Services
{
    public class NewsServices
    {
        private readonly DataContext _context;
        JsonResult success = new JsonResult(new { status = "0" });
        JsonResult fail = new JsonResult(new { status = "1" });
        public NewsServices(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> news(string uuid)
        {
            var user=_context.Notification.SingleOrDefault(x => x.Uuid == uuid);
            var userprofile = _context.UserProfile.SingleOrDefault(x => x.Uuid == uuid);

            var respone = new List<dynamic>
            {
                new
                {
                    id=user.Id,
                    member_id=user.Member_Id,
                    group=user.group,
                    title=user.title,
                    message=user.Message,
                    pushed_at=user.Pushed_At,
                    created_at=user.Created_At,
                    updated_at=user.Updated_At
                }
            };
            JsonResult success = new JsonResult(new { status = "0" ,message="success",respone});
            return success;
        }
    }
}
