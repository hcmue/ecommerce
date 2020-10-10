using AutoMapper;
using MyProjectForJuly2020.Data;
using MyProjectForJuly2020.ViewModels;

namespace MyProjectForJuly2020.Models
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<HangHoa, CartItem>();
            CreateMap<RegisterVM, KhachHang>();
        }
    }
}
