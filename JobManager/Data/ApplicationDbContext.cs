using JobManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<NguoiDung>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<CongViec> CongViec { get; set; }

        public virtual DbSet<CongViec_TaiLieuCV> CongViecTaiLieuCV { get; set; }

        public virtual DbSet<DuAn> DuAn { get; set; }

        public virtual DbSet<NguoiDung> NguoiDung { get; set; }

        public virtual DbSet<NguoiDung_CongViec> NguoiDungCongViec { get; set; }

        public virtual DbSet<TaiLieu> TaiLieu { get; set; }

        public virtual DbSet<TaiLieuCV> TaiLieuCV { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-VCL1NL6;Initial Catalog=JobManager;TrustServerCertificate=True; Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            modelBuilder.Entity<CongViec>(entity =>
            {
                entity.HasKey(e => e.MaCongViec);

                entity.ToTable("CongViec");

                entity.HasOne(d => d.DuAn).WithMany(p => p.CongViec)
                    .HasForeignKey(d => d.MaDuAn);
            });

            modelBuilder.Entity<CongViec_TaiLieuCV>(entity =>
            {
                entity.HasKey(e => e.MaCVTL);

                entity.ToTable("CongViec_TaiLieuCV");

                entity.HasOne(d => d.CongViec).WithMany(p => p.CongViecTaiLieuCV)
                    .HasForeignKey(d => d.MaCongViec);

                entity.HasOne(d => d.TaiLieuCV).WithMany(p => p.CongViecTaiLieuCV)
                    .HasForeignKey(d => d.MaTLCV);
            });

            modelBuilder.Entity<DuAn>(entity =>
            {
                entity.HasKey(e => e.MaDuAn);

                entity.ToTable("DuAn");
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.ToTable("NguoiDung");
            });

            modelBuilder.Entity<NguoiDung_CongViec>(entity =>
            {
                entity.HasKey(e => e.MaNguoiDungCV);

                entity.ToTable("NguoiDung_CongViec");

                entity.HasOne(d => d.CongViec).WithMany(p => p.NguoiDungCongViec)
                    .HasForeignKey(d => d.MaCongViec);

                entity.HasOne(d => d.NguoiDung).WithMany(p => p.NguoiDungCongViec)
                    .HasForeignKey(d => d.NguoiDungId);
            });

            modelBuilder.Entity<TaiLieu>(entity =>
            {
                entity.HasKey(e => e.MaTaiLieu);

                entity.ToTable("TaiLieu");

                entity.HasOne(d => d.DuAn).WithMany(p => p.TaiLieu)
                    .HasForeignKey(d => d.MaDuAn);
            });

            modelBuilder.Entity<TaiLieuCV>(entity =>
            {
                entity.HasKey(e => e.MaTaiLieuCV);

                entity.ToTable("TaiLieuCV");
            });

        }
    }
}