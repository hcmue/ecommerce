using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProjectForJuly2020.Data
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        public int MaKh { get; set; }
        [MaxLength(100)]
        [Required]
        public string HoTen { get; set; }
        [MaxLength(20)]
        [Required]
        public string SoDienThoai { get; set; }
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public string DiaChi { get; set; }
        public bool DangHoatDong { get; set; }
        [MaxLength(10)]
        [Required]
        public string MaNgauNhien { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

    [Table("Role")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [MaxLength(50)]
        [Required]
        public string RoleName { get; set; }
        public bool IsSystem { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

    [Table("UserRole")]
    public class UserRole
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        [ForeignKey("UserId")]
        public KhachHang User { get; set; }
    }
}
