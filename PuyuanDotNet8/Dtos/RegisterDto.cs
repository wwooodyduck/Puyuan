﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PuyuanDotNet8.Dtos
{
    public class RegisterDto
    {
        /*[MaxLength(100)]
        [DefaultValue("root")]
        public string Username { get; set; }
        [MaxLength(100)]
        [DefaultValue("0987654321")]
        public string? Phone { get; set; }
        [EmailAddress]*/
        [MaxLength(100)]
        [DefaultValue("root@mail.com")]
        public string email { get; set; }
        [MaxLength(100)]
        public string password { get; set; }
    }

    public class RegisterComfirmDto
    {
        [MaxLength(100)]
        public string email { get; set; }
    }
}
