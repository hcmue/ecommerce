using System.ComponentModel.DataAnnotations;

namespace MyProjectForJuly2020.ViewModels
{
    public class LoginVM
    {
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
    }
}
