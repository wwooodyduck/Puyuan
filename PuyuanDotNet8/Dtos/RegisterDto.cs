using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PuyuanDotNet8.Dtos
{
    public class RegisterDto
    {
        [MaxLength(100)]
        [DefaultValue("root")]
        public string Username { get; set; }

        [Phone]
        [MaxLength(100)]
        [DefaultValue("0987654321")]
        public string? Phone { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        [DefaultValue("root@mail.com")]
        public string? Email { get; set; }

        [MaxLength(100)]
        [DefaultValue("root")]
        public string Password { get; set; }
    }

    public class RegisterComfirmDto
    {
        [MaxLength(100)]
        [DefaultValue("root")]
        public string Account { get; set; }
    }
}
