using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PuyuanDotNet8.Dtos
{
    public class UsersetDto
    {
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Height{ get; set; }
        public Boolean? Gender { get; set; }
        public string Fcm_Id { get; set; }
        public string Address { get; set; }
        public int? Weight { get; set; }

        
        
        public string Email { get; set; }

        
        
        public string Phone { get; set; }
    }
}
