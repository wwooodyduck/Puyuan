using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PuyuanDotNet8.Dtos
{
    public class ResetPasswordDto
    {
        /*public string Token { get; set; }*/
        [MaxLength(100)]
        [DefaultValue("root")]
        public string Password { get; set; }
    }
}
