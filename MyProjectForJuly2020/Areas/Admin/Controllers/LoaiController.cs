using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyProjectForJuly2020.Data;
using MyProjectForJuly2020.ViewModels;

namespace MyProjectForJuly2020.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaiController : Controller
    {
        private readonly MyDbContext _context;

        public LoaiController(MyDbContext ctx)
        {
            _context = ctx;
        }

        public IActionResult Index()
        {
            var data = _context.Loais.Include(lc => lc.LoaiCha).ToList();
            return View(data);
        }

        public IActionResult Create()
        {

            ViewBag.DanhSachLoaiCha = new LoaiDropDownVM(_context.Loais, "MaLoai", "TenLoai", "MaLoaiCha");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Loai lo)
        {
            if(ModelState.IsValid)
            {
                _context.Add(lo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DanhSachLoaiCha = new LoaiDropDownVM(_context.Loais, "MaLoai", "TenLoai", "MaLoaiCha");
            return View();
        }
    }
}