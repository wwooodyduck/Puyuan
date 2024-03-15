using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace PuyuanDotNet8.Services
{
    public class UsersetService
    {
        private readonly DataContext _context;
        JsonResult success = new JsonResult(new { status = "0" });  
        JsonResult fail = new JsonResult(new { status = "1" });
        public async Task<IActionResult> UserSet(UsersetDto userset,string uuid)
        {
            var user = _context.UserProfile.Include(e => e.UserSet).SingleOrDefault(e => e.Uuid.Equals(uuid));
            if (user == null)
            {
                return fail;
            }
            
        }
    }
}
