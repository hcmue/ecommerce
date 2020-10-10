
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProjectForJuly2020.Data
{
    public class Loai
    {
        public int MaLoai { get; set; }        
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        public int? MaLoaiCha { get; set; }
        [ForeignKey("MaLoaiCha")]
        public Loai LoaiCha { get; set; }
        public ICollection<HangHoa> HangHoas { get; set; }
    }
}
