﻿using AutoMapper;
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
        /*public async Task<IActionResult> share(string uuid)
        {
            
        }*/
    }
}
