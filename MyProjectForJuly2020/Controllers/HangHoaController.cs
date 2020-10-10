using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProjectForJuly2020.Data;
using MyProjectForJuly2020.ViewModels;

namespace MyProjectForJuly2020.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly MyDbContext _context;

        public HangHoaController(MyDbContext ctx)
        {
            _context = ctx;
        }

        public IActionResult Index(int? MaLoai)
        {
            var data = _context.HangHoas.AsQueryable();
            if(MaLoai.HasValue)
            {
                ViewBag.DanhMuc = _context.Loais.FirstOrDefault(lo => lo.MaLoai == MaLoai.Value).TenLoai;

                data = data.Where(hh => hh.MaLoai == MaLoai || hh.Loai.MaLoaiCha == MaLoai);

                //C2
                //List<int> dsLoai = LayDanhSachLoai(MaLoai);
                //data = data.Where(hh => dsLoai.Contains(hh.MaLoai.Value));
            }
           
            var dsHangHoa = data.Select(hh => new HangHoaVM
            {
                MaHh = hh.MaHangHoa,
                TenHh = hh.TenHh,
                DonGia = hh.DonGia,
                GiamGia = hh.GiamGia,
                Hinh = hh.Hinh,
                MoTa = hh.MoTa,
                SoLuong = hh.SoLuong,
                TenLoai = hh.Loai.TenLoai
            }).ToList();
            return View(dsHangHoa);
        }

        private List<int> LayDanhSachLoai(int? maLoai)
        {
            var danhSach = new List<int>();


            return danhSach;
        }

        private void DeQuyTimLoai(int maLoai, List<int> danhSach)
        {
            danhSach.Add(maLoai);
            var loaiCon = _context.Loais
                .Where(lo => lo.MaLoaiCha == maLoai)
                .Select(lo => lo.MaLoai).ToList();
            while(loaiCon.Any())
            {
                var loaiConCanTim = loaiCon.First();
                loaiCon.Remove(loaiConCanTim);

                DeQuyTimLoai(loaiConCanTim, danhSach);
            }
        }

        public IActionResult Detail(Guid id)
        {
            var hh = _context.HangHoas
                .Include(hh => hh.Loai)
                .FirstOrDefault(hh => hh.MaHangHoa == id);
            if(hh == null)
            {
                return Redirect("/Home/PageNotFound");
            }

            return View(hh);
        }
    }
}