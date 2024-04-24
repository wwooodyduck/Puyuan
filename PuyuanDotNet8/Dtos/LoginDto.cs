﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PuyuanDotNet8.Dtos
{
    public class LoginDto
    {
        [MaxLength(100)]
        [DefaultValue("root2")]
        public string email { get; set; }

        [MaxLength(100)]
        [DefaultValue("root2")]
        public string password { get; set; }
    }
}
