﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyProjectForJuly2020.Data;

namespace MyProjectForJuly2020.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyProjectForJuly2020.Data.DonHang", b =>
                {
                    b.Property<Guid>("MaDh")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiaChiGiao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaKh")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayDat")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiNhan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TinhTrangDonHang")
                        .HasColumnType("int");

                    b.Property<double>("TongTien")
                        .HasColumnType("float");

                    b.HasKey("MaDh");

                    b.HasIndex("MaKh");

                    b.ToTable("DonHang");
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.DonHangChiTiet", b =>
                {
                    b.Property<Guid>("MaDh")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaHh")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("DonGia")
                        .HasColumnType("float");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("MaDh", "MaHh");

                    b.HasIndex("MaHh");

                    b.ToTable("DonHangChiTiet");
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.HangHoa", b =>
                {
                    b.Property<Guid>("MaHangHoa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<string>("ChiTiet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("DiemReview")
                        .HasColumnType("float");

                    b.Property<double>("DonGia")
                        .HasColumnType("float");

                    b.Property<byte>("GiamGia")
                        .HasColumnType("tinyint");

                    b.Property<string>("Hinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaLoai")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TenHh")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("MaHangHoa");

                    b.HasIndex("MaLoai");

                    b.HasIndex("TenHh")
                        .IsUnique();

                    b.ToTable("HangHoa");
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.HangHoaTag", b =>
                {
                    b.Property<string>("TagKey")
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("MaHangHoa")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TagKey", "MaHangHoa");

                    b.HasIndex("MaHangHoa");

                    b.ToTable("HangHoaTag");
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.HinhPhu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MaHangHoa")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MaHangHoa");

                    b.ToTable("HinhPhus");
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.KhachHang", b =>
                {
                    b.Property<int>("MaKh")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("DangHoatDong")
                        .HasColumnType("bit");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("MaNgauNhien")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("MatKhau")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("MaKh");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("KhachHang");
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.Loai", b =>
                {
                    b.Property<int>("MaLoai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MaLoaiCha")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenLoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(true);

                    b.HasKey("MaLoai");

                    b.HasIndex("MaLoaiCha");

                    b.ToTable("Loai");
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Criteria")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.ReviewHangHoa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("DiemReview")
                        .HasColumnType("tinyint");

                    b.Property<Guid>("MaHangHoa")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("NgayReview")
                        .HasColumnType("datetime2");

                    b.Property<int>("TieuChi")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaHangHoa");

                    b.HasIndex("TieuChi");

                    b.ToTable("ReviewHangHoas");
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsSystem")
                        .HasColumnType("bit");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.Tag", b =>
                {
                    b.Property<string>("TagKey")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("TagValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagKey");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.DonHang", b =>
                {
                    b.HasOne("MyProjectForJuly2020.Data.KhachHang", "KhachHang")
                        .WithMany()
                        .HasForeignKey("MaKh");
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.DonHangChiTiet", b =>
                {
                    b.HasOne("MyProjectForJuly2020.Data.DonHang", "DonHang")
                        .WithMany()
                        .HasForeignKey("MaDh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyProjectForJuly2020.Data.HangHoa", "HangHoa")
                        .WithMany()
                        .HasForeignKey("MaHh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.HangHoa", b =>
                {
                    b.HasOne("MyProjectForJuly2020.Data.Loai", "Loai")
                        .WithMany("HangHoas")
                        .HasForeignKey("MaLoai")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.HangHoaTag", b =>
                {
                    b.HasOne("MyProjectForJuly2020.Data.HangHoa", "HangHoa")
                        .WithMany("HangHoaTags")
                        .HasForeignKey("MaHangHoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyProjectForJuly2020.Data.Tag", "Tag")
                        .WithMany("HangHoaTags")
                        .HasForeignKey("TagKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.HinhPhu", b =>
                {
                    b.HasOne("MyProjectForJuly2020.Data.HangHoa", "HangHoa")
                        .WithMany("HinhPhus")
                        .HasForeignKey("MaHangHoa");
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.Loai", b =>
                {
                    b.HasOne("MyProjectForJuly2020.Data.Loai", "LoaiCha")
                        .WithMany()
                        .HasForeignKey("MaLoaiCha");
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.ReviewHangHoa", b =>
                {
                    b.HasOne("MyProjectForJuly2020.Data.HangHoa", "HangHoa")
                        .WithMany("ReviewHangHoas")
                        .HasForeignKey("MaHangHoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyProjectForJuly2020.Data.Review", "Review")
                        .WithMany("ReviewHangHoas")
                        .HasForeignKey("TieuChi")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyProjectForJuly2020.Data.UserRole", b =>
                {
                    b.HasOne("MyProjectForJuly2020.Data.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyProjectForJuly2020.Data.KhachHang", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
