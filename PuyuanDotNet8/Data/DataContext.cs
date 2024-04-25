using Microsoft.EntityFrameworkCore;
using PuyuanDotNet8.Data;

namespace PuyuanDotNet8.Data;

public partial class DataContext : DbContext
{   
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public virtual DbSet<UserProfile> UserProfile { get; set; }
    public virtual DbSet<UserSet> UserSet { get; set; }
    public virtual DbSet<Default> Default { get; set; }
    public virtual DbSet<Setting> Setting { get; set; }
    public virtual DbSet<Notification> Notification { get; set; }
    public virtual DbSet<Share> Share { get; set; }
    public virtual DbSet<BloodPressure> BloodPressure { get; set; }
    public virtual DbSet<_Weight> _Weight { get; set; }
    public virtual DbSet<BloodSugar> BloodSugar { get; set; }
    public virtual DbSet<DiaryDiet> DiaryDiet { get; set; }
    public virtual DbSet<UserCare> UserCare { get; set; }
    public virtual DbSet<HbA1c> HbA1c { get; set; }
    public virtual DbSet<MedicalInformation> MedicalInformation { get; set; }
    public virtual DbSet<DrugInformation> DrugInformation { get; set; }
    public virtual DbSet<Friend> Friend { get; set; }
    public virtual DbSet<Verification> Verifications { get; set; }
}
