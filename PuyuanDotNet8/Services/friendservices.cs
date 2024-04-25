using Azure;
using Microsoft.AspNetCore.Mvc;

namespace PuyuanDotNet8.Services
{
    public class friendservices
    {
        private readonly DataContext _context;
        public friendservices(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> codeget(string uuid)
        {
            var userset=_context.UserSet.SingleOrDefault(e => e.Uuid.Equals(uuid));
            var resopne = new
            {
               status="0",
               message="成功",
               invite_code= userset.Invite_Code
            };
            JsonResult success = new JsonResult(resopne);
            return success;
        }

        public async Task<IActionResult> groupteam(string uuid)
        {
            var userset = _context.UserSet.SingleOrDefault(e => e.Uuid.Equals(uuid));
            var share=_context.Share.Where(e => e.Uid.Equals(uuid)).SingleOrDefault();

            var respone = new
            {
                status="0",
                message="成功",
                friend = new 
                {
                    id=share.Id,
                    name=userset.Name,
                    relation_type=share.Relation_Type
                }
            };

            JsonResult success= new JsonResult(respone);
            return success;
        }

        public async Task<IActionResult> teaminvited(string uuid)
        {
            var friend = _context.Friend.SingleOrDefault(e => e.User_Id.Equals(uuid));
            var userset = _context.UserSet.SingleOrDefault(e => e.Uuid.Equals(uuid));

            var respone = new
            {
                id=friend.Id,
                user_id=friend.User_Id,
                relation_id=friend.Relation_Id,
                type=friend.Friend_Type,
                read=friend.Read,
                status=friend.Status,
                created_at=friend.Created_At,
                updated_at=friend.Updated_At,
                user=new
                {
                    id=userset.Uuid,
                    name=userset.Name,
                    
                }
            };

            JsonResult success=new JsonResult(respone);
            return success;
        }
    }
}
