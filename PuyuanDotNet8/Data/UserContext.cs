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
            entity.Property(e => e.Status).HasDefaultValue("Normal");
            entity.Property(e => e.UnreadRecordsOne).HasDefaultValue(0);
            entity.Property(e => e.UnreadRecordsTwo).HasDefaultValue("0");
            entity.Property(e => e.UnreadRecordsThree).HasDefaultValue(0);
            entity.Property(e => e.Verified).HasDefaultValue(false);
            entity.Property(e => e.Privacy_Policy).HasDefaultValue(false);
            entity.Property(e => e.Must_Change_Password).HasDefaultValue(false);
            entity.Property(e => e.Badge).HasDefaultValue(0);
            entity.Property(e => e.Login_Times).HasDefaultValue(0);
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
    }
}
