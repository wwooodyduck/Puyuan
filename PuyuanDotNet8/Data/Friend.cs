using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuyuanDotNet8.Data;

[Table("Friend")]
public partial class Friend
{
    [Key]
    public int Id { get; set; }
    public int User_Id { get; set; }
    public int Relation_Id { get; set; }
    public int Friend_Type { get; set; }
    public int Status { get; set; }

    public bool? Read { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime? Updated_At { get; set; }

    public UserProfile UserProfile { get; set; }
}
