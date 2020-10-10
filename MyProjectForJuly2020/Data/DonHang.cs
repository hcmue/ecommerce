using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProjectForJuly2020.Data
{
    [Table("DonHang")]
    public class DonHang
    {
        [Key]
        public Guid MaDh { get; set; }
        public DateTime NgayDat { get; set; }
        public int? MaKh { get; set; }
        [ForeignKey("MaKh")]
        public KhachHang KhachHang { get; set; }
        public string NguoiNhan { get; set; }
        public string DiaChiGiao { get; set; }
        public double TongTien { get; set; }
        public TinhTrangDonHang TinhTrangDonHang { get; set; }
    }

    [Table("DonHangChiTiet")]
    public class DonHangChiTiet
    {
        public Guid MaDh { get; set; }
        public Guid MaHh { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        [ForeignKey("MaDh")]
        public DonHang DonHang { get; set; }
        [ForeignKey("MaHh")]
        public HangHoa HangHoa { get; set; }
    }

    public enum TinhTrangDonHang
    {
        MoiDatHang = 1, 
        DaXacNhan = 2,
        DaThanhToan = 3,
        DaGiaoHang = 4,
        HuyDonHang = 5
    }
}
