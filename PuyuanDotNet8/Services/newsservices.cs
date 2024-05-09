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
        /*public async Task<IActionResult> news(string uuid)
        {

        }*/
    }
}
