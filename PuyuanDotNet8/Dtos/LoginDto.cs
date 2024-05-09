using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PuyuanDotNet8.Dtos
{
    public class LoginDto
    {
        [MaxLength(100)]
        [DefaultValue("y97213h53@gmail.com")]
        public string email { get; set; }

        [MaxLength(100)]
        [DefaultValue("root")]
        public string password { get; set; }
    }
}
