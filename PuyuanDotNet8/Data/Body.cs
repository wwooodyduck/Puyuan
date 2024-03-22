using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PuyuanDotNet8.Data;

[Table("BloodPressure")]
public partial class BloodPressure
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Uuid { get; set; }
    [MaxLength(3)]
    public double? Systolic { get; set; }
    [MaxLength(3)]
    public double? Diastolic { get; set; }
    [MaxLength(3)]
    public int? Pulse { get; set; }
    public DateTime Recorded_At { get; set; }

    public UserProfile UserProfile { get; set; }
}

[Table("Weight")]
public partial class _Weight
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Uuid { get; set; }
    [MaxLength(3)]
    public double? Weight { get; set; }
    [MaxLength(3)]
    public double? Body_Fat { get; set; }
    [MaxLength(3)]
    public double? Bmi { get; set; }
    public DateTime Recorded_At { get; set; }

    public UserProfile UserProfile { get; set; }
}

[Table("BloodSugar")]
public partial class BloodSugar
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Uuid { get; set; }
    [MaxLength(3)]
    public int? Sugar { get; set; }
    [MaxLength(3)]
    public int? Timeperiod { get; set; }
    public DateTime Recorded_At { get; set; }

    public UserProfile UserProfile { get; set; }
}

[Table("DiaryDiet")]
public partial class DiaryDiet
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Uuid { get; set; }
    [MaxLength(5)]
    public string? Description { get; set; }
    [MaxLength(5)]
    public int? Meal { get; set; }
    [MaxLength(100)]
    public string? Tag { get; set; }
    public string? Image { get; set; }
    public int? ImageCount { get; set; }
    [MaxLength(100)]
    public double? Lat { get; set; }
    [MaxLength(100)]
    public double? Lng { get; set; }

    public DateTime Recorded_At { get; set; }

    public UserProfile UserProfile { get; set; }
}

[Table("UserCare")]
public partial class UserCare
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
    public DateTime Created_At { get; set; }
    public DateTime? Updated_At { get; set; }

    public UserProfile UserProfile { get; set; }
}

[Table("HbA1c")]
public partial class HbA1c
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Uuid { get; set; }
    [MaxLength(20)]
    public double? A1c { get; set; }
    public DateTime Recorded_At { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime? Updated_At { get; set; }
    [JsonIgnore]
    public UserProfile UserProfile { get; set; }
}

[Table("MedicalInformation")]
public partial class MedicalInformation
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Uuid { get; set; }
    public int? Diabetes_Type { get; set; }
    public bool? Oad { get; set; }
    public bool? Insulin { get; set; }
    public bool? Anti_Hypertensives { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime? Updated_At { get; set; }

    public UserProfile UserProfile { get; set; }
}

[Table("DrugInformation")]
public partial class DrugInformation
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Uuid { get; set; }
    public bool? Drug_Type { get; set; }
    [MaxLength(100)]
    public string? Name { get; set; }
    public DateTime Recorded_At { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime? Updated_At { get; set; }

    public UserProfile UserProfile { get; set; }
}
