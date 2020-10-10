using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProjectForJuly2020.ViewModels
{
    public class HangHoaVM
    {
        public Guid MaHh { get; set; }
        public string TenHh { get; set; }
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }
        public double GiaBan => DonGia * (100 - GiamGia) / 100.0;
        public int SoLuong { get; set; }
        public string Hinh { get; set; }        
        public string MoTa { get; set; }        
        public string TenLoai { get; set; }
    }
}
