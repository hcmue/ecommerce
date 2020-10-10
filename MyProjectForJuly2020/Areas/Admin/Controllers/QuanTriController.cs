using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyProjectForJuly2020.Data;
using MyProjectForJuly2020.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyProjectForJuly2020.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class QuanTriController : Controller
    {
        private readonly MyDbContext _context;

        public QuanTriController(MyDbContext db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            if (User.IsInRole("Quản trị Hệ thống"))
            {
                
            }
            return View();
        }

        //[Authorize(Roles ="Quản trị Hệ thống"), HttpGet]
        public IActionResult PhanQuyen()
        {
            var data = _context.KhachHangs.Include(kh => kh.UserRoles)
                .Select(kh => new PhanQuyenVM { 
                    MaKh = kh.MaKh,
                    HoTen = kh.HoTen,
                    QuanTri = kh.UserRoles.FirstOrDefault(ur => ur.RoleId == 1) != null,
                    BanHang = kh.UserRoles.FirstOrDefault(ur => ur.RoleId == 2) != null,
                    ThuKho = kh.UserRoles.FirstOrDefault(ur => ur.RoleId == 3) != null,
                    KhachHang = kh.UserRoles.FirstOrDefault(ur => ur.RoleId == 4) != null
                });
            return View(data);
        }

        //[Authorize(Roles ="Quản trị Hệ thống"),
        [HttpPost]
        public IActionResult PhanQuyen(List<int> MaKh, List<bool> QuanTri, List<bool> ThuKho, List<bool> BanHang, List<bool> KhachHang)
        {
            return RedirectToAction("PhanQuyen");
        }
    }
}