using System.ComponentModel.DataAnnotations;

namespace MyProjectForJuly2020.ViewModels
{
    public class RegisterVM
    {
        [MaxLength(100)]
        [Required]
        [Display(Name ="Họ tên")]
        public string HoTen { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }
        [Compare("MatKhau", ErrorMessage ="Mật khẩu không khớp")]
        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        public string NhapLaiMatKhau { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
    }
}
