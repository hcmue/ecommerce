using System;

namespace MyProjectForJuly2020.ViewModels
{
    public class CartItem
    {
        public Guid MaHangHoa { get; set; }
        public string TenHh { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien => DonGia * SoLuong;
    }
}
