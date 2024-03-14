using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PuyuanDotNet8.Data;
namespace PuyuanDotNet8.Data;

public partial class UserProfile
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Username { get; set; }
    [MaxLength(100)]
    public string Uuid { get; set; }
    [MaxLength(100)]
    public string? Phone { get; set; }
    [MaxLength(100)]
    public string? Email { get; set; }
    [MaxLength(256)]
    public string Password { get; set; }
    [MaxLength(100)]
    public string? Fb_Id { get; set; }

    public DateTime Created_At { get; set; }
    public DateTime? Updated_At { get; set; }

    public UserSet UserSet { get; set; }
    public Default Default { get; set; }
    public Setting Setting { get; set; }
    public ICollection<Notification>? Notifications { get; set; }
    public ICollection<Share>? Shares { get; set; }
    public Verification? Verification { get; set; }
    public ICollection<BloodPressure>? BloodPressures { get; set; }
    public ICollection<_Weight>? _Weights { get; set; }
    public ICollection<BloodSugar>? BloodSugars { get; set; }
    public ICollection<DiaryDiet>? DiaryDiets { get; set; }
    public ICollection<UserCare>? UserCares { get; set; }
    public ICollection<HbA1c>? HbA1Cs { get; set; }
    public MedicalInformation MedicalInformation { get; set; }
    public ICollection<DrugInformation>? DrugInformation { get; set; }
    public ICollection<Friend>? Friends { get; set; }
}

[Table("UserSet")]
public partial class UserSet
{
    [Key]
    [MaxLength(100)]
    public string Uuid { get; set; }

    [MaxLength(100)]
    public string? Name { get; set; }

    [MaxLength(100)]
    public string Status { get; set; }
    [MaxLength(100)]
    public string? Group { get; set; }
    public DateTime? Birthday { get; set; }
    [MaxLength(10),Precision(5)]
    public int? Height { get; set; }
    [MaxLength(10), Precision(5)]
    public int? Weight { get; set; }
    public bool? Gender { get; set; }
    [MaxLength(100)]
    public string? Address { get; set; }
    [MaxLength(100)]
    public string Invite_Code { get; set; }
    [MaxLength(10)]
    public int UnreadRecordsOne { get; set; }
    [MaxLength(100)]
    public string UnreadRecordsTwo { get; set; }
    [MaxLength(10)]
    public int UnreadRecordsThree { get; set; }
    public bool Verified { get; set; }
    public bool Privacy_Policy { get; set; }
    public bool Must_Change_Password { get; set; }
    [MaxLength(100)]
    public string? Fcm_Id { get; set; }
    [MaxLength(10)]
    public int Badge { get; set; }
    [MaxLength(20)]
    public int Login_Times { get; set; }

    public DateTime Created_At { get; set; }
    public DateTime? Updated_At { get; set; }

    [NotMapped]
    public UserProfile UserProfile { get; set; }
}

[Table("Default")]
public partial class Default
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Uuid { get; set; }
    [MaxLength(5)]
    public int? Suger_Delta_Max { get; set; }
    [MaxLength(5)]
    public int? Suger_Delta_Min { get; set; }
    [MaxLength(5)]
    public int? Suger_Morning_Max { get; set; }
    [MaxLength(5)]
    public int? Suger_Morning_Min { get; set; }
    [MaxLength(5)]
    public int? Suger_Evening_Max { get; set; }
    [MaxLength(5)]
    public int? Suger_Evening_Min { get; set; }
    [MaxLength(5)]
    public int? Suger_Before_Max { get; set; }
    [MaxLength(5)]
    public int? Suger_Before_Min { get; set; }
    [MaxLength(5)]
    public int? Suger_After_Max { get; set; }
    [MaxLength(5)]
    public int? Suger_After_Min { get; set; }
    [MaxLength(5)]
    public int? Systolic_Max { get; set; }
    [MaxLength(5)]
    public int? Systolic_Min { get; set; }
    [MaxLength(5)]
    public int? Diastolic_Max { get; set; }
    [MaxLength(5)]
    public int? Diastolic_Min { get; set; }
    [MaxLength(5)]
    public int? Pulse_Max { get; set; }
    [MaxLength(5)]
    public int? Pulse_Min { get; set; }
    [MaxLength(5)]
    public int? Weight_Max { get; set; }
    [MaxLength(5)]
    public int? Weight_Min { get; set; }
    [MaxLength(5)]
    public int? Bmi_Max { get; set; }
    [MaxLength(5)]
    public int? Bmi_Min { get; set; }
    [MaxLength(5)]
    public int? Body_Fat_Max { get; set; }
    [MaxLength(5)]
    public int? Body_Fat_Min { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime? Updated_At { get; set; }

    public UserProfile UserProfile { get; set; }
}

[Table("Setting")]
public partial class Setting
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Uuid { get; set; }
    public bool After_Recording { get; set; }
    public bool No_Recording_For_A_Day { get; set; }
    public bool Over_Max_Or_Under_Min { get; set; }
    public bool After_Meal { get; set; }
    public bool Unit_Of_Sugar { get; set; }
    public bool Unit_Of_Weight { get; set; }
    public bool Unit_Of_Height { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime? Updated_At { get; set; }

    public UserProfile UserProfile { get; set; }
}

[Table("Notification")]
public partial class Notification
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Uuid { get; set; }
    public int? User_Id { get; set; }
    public int? Member_Id { get; set; }
    public int? Reply_Id { get; set; }
    [MaxLength(100)]
    public string? Message { get; set; }
    public DateTime? Created_At { get; set; }
    public DateTime? Updated_At { get; set; }

    public UserProfile UserProfile { get; set; }

}

[Table("Share")]
public partial class Share
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Uid { get; set; }
    [MaxLength(100)]
    public int Fid { get; set; }
    [MaxLength(100)]
    public int Data_Type { get; set; }
    [MaxLength(100)]
    public int Relation_Type { get; set; }

    public UserProfile UserProfile { get; set; }
}

[Table("Verification")]
public partial class Verification
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Uuid { get; set; }
    [MaxLength(100)]
    public string VerifictionCode { get; set; }

    public virtual UserProfile UserProfile { get; set; }
}
