using Microsoft.EntityFrameworkCore;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace komatsu.api.DBContexts
{
    public partial class devkomatsuContext : DbContext
    {
        public devkomatsuContext()
        {
        }
        public devkomatsuContext(DbContextOptions<devkomatsuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<MasterModel> MasterModel { get; set; }
        public virtual DbSet<SystemVersion> SystemVersion { get; set; }
        public virtual DbSet<MasterTransaction> MasterTransaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=endpoint.ops.fysvc.com;port=3306;user=dev-komatsu;password=3e9adb8f474e926dee112d726eae399934ae7ab0ad23474190506564d1fc3eaf;database=dev-komatsu");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Device)
                    .HasColumnName("device")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DeviceId)
                    .HasColumnName("device_id")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsDelete)
                    .HasColumnName("is_delete")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsLogin)
                    .HasColumnName("is_login")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Lastname)
                    .HasColumnName("lastname")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("sha256");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<MasterModel>(entity =>
            {
                entity.ToTable("master_model");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ModelGroup)
                    .HasColumnName("model_group")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<SystemVersion>(entity =>
            {
                entity.ToTable("system_version");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Device)
                    .HasColumnName("device")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsForce)
                     .HasColumnName("is_force")
                     .HasColumnType("tinyint(1)")
                     .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StoreLink)
                    .HasColumnName("store_link")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreatedAt)
                     .HasColumnName("created_at")
                     .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<MasterTransaction>(entity =>
            {
                entity.ToTable("master_transaction");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                  .HasColumnName("user_id")
                  .HasColumnType("int(11)");

                entity.Property(e => e.Param)
                    .HasColumnName("param")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("'NULL'");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
