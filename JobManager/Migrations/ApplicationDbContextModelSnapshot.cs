﻿// <auto-generated />
using System;
using JobManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JobManager.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JobManager.Models.CongViec", b =>
                {
                    b.Property<string>("MaCongViec")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("MaDuAn")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MoTaCongViec")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MucDoUuTien")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTaoCongViec")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenCongViec")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaCongViec");

                    b.HasIndex("MaDuAn");

                    b.ToTable("CongViec", (string)null);
                });

            modelBuilder.Entity("JobManager.Models.CongViec_TaiLieuCV", b =>
                {
                    b.Property<string>("MaCVTL")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MaCongViec")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MaTLCV")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaCVTL");

                    b.HasIndex("MaCongViec");

                    b.HasIndex("MaTLCV");

                    b.ToTable("CongViec_TaiLieuCV", (string)null);
                });

            modelBuilder.Entity("JobManager.Models.DuAn", b =>
                {
                    b.Property<string>("MaDuAn")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("MoTaDuAn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTaoDuAn")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenDuAn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaDuAn");

                    b.ToTable("DuAn", (string)null);
                });

            modelBuilder.Entity("JobManager.Models.NguoiDung", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DisableAccount")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("GioiTinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoNguoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenNguoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("NguoiDung", (string)null);
                });

            modelBuilder.Entity("JobManager.Models.NguoiDung_CongViec", b =>
                {
                    b.Property<string>("MaNguoiDungCV")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MaCongViec")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NguoiDungId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaNguoiDungCV");

                    b.HasIndex("MaCongViec");

                    b.HasIndex("NguoiDungId");

                    b.ToTable("NguoiDung_CongViec", (string)null);
                });

            modelBuilder.Entity("JobManager.Models.TaiLieu", b =>
                {
                    b.Property<string>("MaTaiLieu")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MaDuAn")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TenTaiLieu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaTaiLieu");

                    b.HasIndex("MaDuAn");

                    b.ToTable("TaiLieu", (string)null);
                });

            modelBuilder.Entity("JobManager.Models.TaiLieuCV", b =>
                {
                    b.Property<string>("MaTaiLieuCV")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TenTaiLieuCV")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaTaiLieuCV");

                    b.ToTable("TaiLieuCV", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("JobManager.Models.CongViec", b =>
                {
                    b.HasOne("JobManager.Models.DuAn", "DuAn")
                        .WithMany("CongViec")
                        .HasForeignKey("MaDuAn");

                    b.Navigation("DuAn");
                });

            modelBuilder.Entity("JobManager.Models.CongViec_TaiLieuCV", b =>
                {
                    b.HasOne("JobManager.Models.CongViec", "CongViec")
                        .WithMany("CongViecTaiLieuCV")
                        .HasForeignKey("MaCongViec");

                    b.HasOne("JobManager.Models.TaiLieuCV", "TaiLieuCV")
                        .WithMany("CongViecTaiLieuCV")
                        .HasForeignKey("MaTLCV");

                    b.Navigation("CongViec");

                    b.Navigation("TaiLieuCV");
                });

            modelBuilder.Entity("JobManager.Models.NguoiDung_CongViec", b =>
                {
                    b.HasOne("JobManager.Models.CongViec", "CongViec")
                        .WithMany("NguoiDungCongViec")
                        .HasForeignKey("MaCongViec");

                    b.HasOne("JobManager.Models.NguoiDung", "NguoiDung")
                        .WithMany("NguoiDungCongViec")
                        .HasForeignKey("NguoiDungId");

                    b.Navigation("CongViec");

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("JobManager.Models.TaiLieu", b =>
                {
                    b.HasOne("JobManager.Models.DuAn", "DuAn")
                        .WithMany("TaiLieu")
                        .HasForeignKey("MaDuAn");

                    b.Navigation("DuAn");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("JobManager.Models.NguoiDung", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("JobManager.Models.NguoiDung", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobManager.Models.NguoiDung", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("JobManager.Models.NguoiDung", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobManager.Models.CongViec", b =>
                {
                    b.Navigation("CongViecTaiLieuCV");

                    b.Navigation("NguoiDungCongViec");
                });

            modelBuilder.Entity("JobManager.Models.DuAn", b =>
                {
                    b.Navigation("CongViec");

                    b.Navigation("TaiLieu");
                });

            modelBuilder.Entity("JobManager.Models.NguoiDung", b =>
                {
                    b.Navigation("NguoiDungCongViec");
                });

            modelBuilder.Entity("JobManager.Models.TaiLieuCV", b =>
                {
                    b.Navigation("CongViecTaiLieuCV");
                });
#pragma warning restore 612, 618
        }
    }
}
