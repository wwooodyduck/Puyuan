using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PuyuanDotNet8.Dtos
{
    public class VerificationDto { }

    public class CheckVerificationDto
    {
        [Phone]
        [MaxLength(100)]
        [DefaultValue("0987654321")]
        public string Phone { get; set; }

        [Display(Name = "code")]
        [MaxLength(100)]
        public string VerifictionCode { get; set; }
    }

    public class SendVerificationDto
    {
        [EmailAddress]
        [MaxLength(100)]
        [DefaultValue("root@mail.com")]
        public string Email { get; set; }

        [Phone]
        [MaxLength(100)]
        [DefaultValue("0987654321")]
        public string Phone { get; set; }
    }
}
