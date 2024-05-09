    using Microsoft.EntityFrameworkCore;
using PuyuanDotNet8.Data;

namespace PuyuanDotNet8.Data;

public partial class DataContext : DbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.HasIndex(e => e.Username).IsUnique();
            entity.Property(e => e.Fb_Id).HasDefaultValue("");
            entity.Property(e => e.Username).HasDefaultValue("");
            entity.HasOne(c => c.UserSet).WithOne(e => e.UserProfile).HasForeignKey<UserSet>(a => a.Uuid).HasPrincipalKey<UserProfile>(s => s.Uuid);
            entity.HasOne(c => c.Verification).WithOne(e => e.UserProfile).HasForeignKey<Verification>(a => a.Uuid).HasPrincipalKey<UserProfile>(s => s.Uuid);
            entity.HasOne(c => c.Default).WithOne(e => e.UserProfile).HasForeignKey<Default>(a => a.Uuid).HasPrincipalKey<UserProfile>(s => s.Uuid);
            entity.HasOne(c => c.Setting).WithOne(e => e.UserProfile).HasForeignKey<Setting>(a => a.Uuid).HasPrincipalKey<UserProfile>(s => s.Uuid);
            entity.HasMany(c => c.Notifications).WithOne(e => e.UserProfile).HasForeignKey(a => a.Uuid).HasPrincipalKey(s => s.Uuid);
            entity.HasMany(c => c.Shares).WithOne(e => e.UserProfile).HasForeignKey(a => a.Uid).HasPrincipalKey(s => s.Uuid);
            entity.HasMany(c => c.BloodPressures).WithOne(e => e.UserProfile).HasForeignKey(a => a.Uuid).HasPrincipalKey(s => s.Uuid);
            entity.HasMany(c => c._Weights).WithOne(e => e.UserProfile).HasForeignKey(a => a.Uuid).HasPrincipalKey(s => s.Uuid);
            entity.HasMany(c => c.BloodSugars).WithOne(e => e.UserProfile).HasForeignKey(a => a.Uuid).HasPrincipalKey(s => s.Uuid);
            entity.HasMany(c => c.DiaryDiets).WithOne(e => e.UserProfile).HasForeignKey(a => a.Uuid).HasPrincipalKey(s => s.Uuid);
            entity.HasMany(c => c.UserCares).WithOne(e => e.UserProfile).HasForeignKey(a => a.Uuid).HasPrincipalKey(s => s.Uuid);
            entity.HasMany(c => c.HbA1Cs).WithOne(e => e.UserProfile).HasForeignKey(a => a.Uuid).HasPrincipalKey(s => s.Uuid);
            entity.HasOne(c => c.MedicalInformation).WithOne(e => e.UserProfile).HasForeignKey<MedicalInformation>(a => a.Uuid).HasPrincipalKey<UserProfile>(s => s.Uuid);
            entity.HasMany(c => c.DrugInformation).WithOne(e => e.UserProfile).HasForeignKey(a => a.Uuid).HasPrincipalKey(s => s.Uuid);
            entity.HasMany(c => c.Friends).WithOne(e => e.UserProfile).HasForeignKey(a => a.User_Id).HasPrincipalKey(s => s.Id);

        });

        modelBuilder.Entity<UserSet>(entity =>
        {
            entity.Property(e => e.Status).HasDefaultValue("VIP");
            entity.Property(e => e.Name).HasDefaultValue("");
            entity.Property(e => e.Address).HasDefaultValue("");
            entity.Property(e => e.Group).HasDefaultValue("");
            entity.Property(e => e.Birthday).HasDefaultValue(DateTime.MinValue);
            entity.Property(e => e.Height).HasDefaultValue(0.0);
            entity.Property(e => e.Weight).HasDefaultValue("0");
            entity.Property(e => e.Gender).HasDefaultValue(false);
            entity.Property(e => e.Fcm_Id).HasDefaultValue("");
            entity.Property(e => e.UnreadRecordsOne).HasDefaultValue(0);
            entity.Property(e => e.UnreadRecordsTwo).HasDefaultValue(0);
            entity.Property(e => e.UnreadRecordsThree).HasDefaultValue(0);
            entity.Property(e => e.Verified).HasDefaultValue(false);
            entity.Property(e => e.Privacy_Policy).HasDefaultValue(false);
            entity.Property(e => e.Must_Change_Password).HasDefaultValue(false);
            entity.Property(e => e.Badge).HasDefaultValue(0);
            entity.Property(e => e.login_times).HasDefaultValue(0);
            entity.Property(e => e.Updated_At).HasDefaultValue(DateTime.MinValue);
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.Property(e => e.After_Recording).HasDefaultValue(false);
            entity.Property(e => e.No_Recording_For_A_Day).HasDefaultValue(false);
            entity.Property(e => e.Over_Max_Or_Under_Min).HasDefaultValue(false);
            entity.Property(e => e.After_Meal).HasDefaultValue(false);
            entity.Property(e => e.Unit_Of_Sugar).HasDefaultValue(false);
            entity.Property(e => e.Unit_Of_Weight).HasDefaultValue(false);
            entity.Property(e => e.Unit_Of_Height).HasDefaultValue(false);
            entity.Property(e => e.Updated_At).HasDefaultValue(DateTime.MinValue);
        });

        modelBuilder.Entity<BloodPressure>(entity =>
        {
            entity.Property(e => e.Systolic).HasDefaultValue(0d);
            entity.Property(e => e.Diastolic).HasDefaultValue(0d);
            entity.Property(e => e.Pulse).HasDefaultValue(0);
        });

        modelBuilder.Entity<_Weight>(entity =>
        {
            entity.Property(e => e.Weight).HasDefaultValue(0d);
            entity.Property(e => e.Body_Fat).HasDefaultValue(0d);
            entity.Property(e => e.Bmi).HasDefaultValue(0d);
        });

        modelBuilder.Entity<BloodSugar>(entity =>
        {
            entity.Property(e => e.Sugar).HasDefaultValue(0);
            entity.Property(e => e.Timeperiod).HasDefaultValue(0);
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.Property(e => e.Status).HasDefaultValue(0);
        });
        modelBuilder.Entity<Default>(entity =>
        {
            entity.Property(e => e.Uuid).HasDefaultValue("");
            entity.Property(e => e.Sugar_Delta_Max).HasDefaultValue(1.0);
            entity.Property(e => e.Sugar_Delta_Min).HasDefaultValue(1.0);
            entity.Property(e => e.Sugar_Morning_Max).HasDefaultValue(1.0);
            entity.Property(e => e.Sugar_Morning_Min).HasDefaultValue(1.0);
            entity.Property(e => e.Sugar_Evening_Max).HasDefaultValue(1.0);
            entity.Property(e => e.Sugar_Evening_Min).HasDefaultValue(1.0);
            entity.Property(e => e.Sugar_Before_Min).HasDefaultValue(1.0);
            entity.Property(e => e.Sugar_Before_Max).HasDefaultValue(1.0);
            entity.Property(e => e.Sugar_After_Max).HasDefaultValue(1.0);
            entity.Property(e => e.Sugar_After_Min).HasDefaultValue(1.0);
            entity.Property(e => e.Systolic_Max).HasDefaultValue(0);
            entity.Property(e => e.Systolic_Min).HasDefaultValue(0);
            entity.Property(e => e.Diastolic_Max).HasDefaultValue(0);
            entity.Property(e => e.Diastolic_Min).HasDefaultValue(0);
            entity.Property(e => e.Pulse_Min).HasDefaultValue(0);
            entity.Property(e => e.Pulse_Max).HasDefaultValue(0);
            entity.Property(e => e.Weight_Max).HasDefaultValue(1.0);
            entity.Property(e => e.Weight_Min).HasDefaultValue(1.0);
            entity.Property(e => e.Bmi_Max).HasDefaultValue(1.0);
            entity.Property(e => e.Bmi_Min).HasDefaultValue(1.0);
            entity.Property(e => e.Body_Fat_Max).HasDefaultValue(1.0);
            entity.Property(e => e.Body_Fat_Min).HasDefaultValue(1.0);
            entity.Property(e => e.Updated_At).HasDefaultValue(DateTime.MinValue);
        });
        modelBuilder.Entity<MedicalInformation>(entity =>
        {
            entity.Property(e => e.Uuid).HasDefaultValue("");
            entity.Property(e => e.Diabetes_Type).HasDefaultValue(0);
            entity.Property(e => e.Oad).HasDefaultValue(false);
            entity.Property(e => e.Insulin).HasDefaultValue(false);
            entity.Property(e => e.Anti_Hypertensives).HasDefaultValue(false);
            entity.Property(e => e.Updated_At).HasDefaultValue(DateTime.MinValue);
        });
        modelBuilder.Entity<HbA1c>(entity =>
        {
            entity.Property(e => e.Uuid).HasDefaultValue("");
            entity.Property(e => e.A1c).HasDefaultValue(0.0);
            entity.Property(e => e.Recorded_At).HasDefaultValue(DateTime.MinValue);
            entity.Property(e => e.Created_At).HasDefaultValue(DateTime.MinValue);
            entity.Property(e => e.Updated_At).HasDefaultValue(DateTime.MinValue);
        });
    }
}
