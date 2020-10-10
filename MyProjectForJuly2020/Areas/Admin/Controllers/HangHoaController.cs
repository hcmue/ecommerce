using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyProjectForJuly2020.Data;
using MyProjectForJuly2020.Helpers;
using MyProjectForJuly2020.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyProjectForJuly2020.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HangHoaController : Controller
    {
        private readonly ILogger _logger;
        private readonly MyDbContext _context;

        public HangHoaController(MyDbContext ctx, ILogger<HangHoaController> logger)
        {
            _context = ctx;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.HangHoas
                .Include(hh => hh.Loai)
                .ToListAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.DanhSachLoai = new LoaiDropDownVM(_context.Loais, "MaLoai", "TenLoai", "MaLoai");
            return View();
        }

        [HttpPost]
        public IActionResult Create(HangHoa hh, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var urlHinh = FileHelper.UploadFileToFolder(Hinh, "HangHoa");
                    hh.Hinh = urlHinh;
                    _context.Add(hh);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Loi: {ex.Message}");

                    ViewBag.ThongBaoLoi = "Có lỗi";
                    ViewBag.DanhSachLoai = new LoaiDropDownVM(_context.Loais, "MaLoai", "TenLoai", "MaLoai");
                    return View();
                }
            }

            ViewBag.DanhSachLoai = new LoaiDropDownVM(_context.Loais, "MaLoai", "TenLoai", "MaLoai");
            return View();
        }

        public IActionResult Edit(Guid id)
        {
            var hh = _context.HangHoas.FirstOrDefault(h => h.MaHangHoa == id);

            ViewBag.DanhSachLoai = new LoaiDropDownVM(_context.Loais, "MaLoai", "TenLoai", "MaLoai", hh.MaLoai);
            return View(hh);
        }
    }
}