using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProjectForJuly2020.Data;
using MyProjectForJuly2020.Helpers;
using MyProjectForJuly2020.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyProjectForJuly2020.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public KhachHangController(MyDbContext ctx, IMapper mapper)
        {
            _context = ctx; _mapper = mapper;
        }

        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var khachHang = _mapper.Map<KhachHang>(model);
                        khachHang.MaNgauNhien = MyTools.GetRandom();
                        khachHang.MatKhau = model.MatKhau.ToSHA512Hash(khachHang.MaNgauNhien);
                        _context.Add(khachHang);
                        _context.SaveChanges();

                        //Add role for user, default Customer
                        var userRole = new UserRole
                        {
                            RoleId = 4,//Khách hàng
                            UserId = khachHang.MaKh
                        };
                        _context.Add(userRole);
                        _context.SaveChanges();

                        transaction.Commit();
                        return RedirectToAction("DangNhap");
                    }
                    catch
                    {
                        transaction.Rollback();
                        return View();
                    }
                }

            }
            return View();
        }

        #region Dang Nhap
        [HttpGet]
        public IActionResult DangNhap(string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            string thongBaoLoi = string.Empty;
            if (ModelState.IsValid)
            {
                var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.Email == model.Email);
                if (khachHang == null)
                {
                    ViewBag.ThongBaoLoi = "Tài khoản không tồn tại.";
                    return View();
                }
                if (!khachHang.DangHoatDong)
                {
                    ViewBag.ThongBaoLoi = "Tài khoản đang bị khóa.";
                    return View();
                }
                if (khachHang.MatKhau != model.MatKhau.ToSHA512Hash(khachHang.MaNgauNhien))
                {
                    ViewBag.ThongBaoLoi = "Sai thông tin đăng nhập.";
                    return View();
                }

                //set các claims
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, khachHang.HoTen),
                    new Claim(ClaimTypes.Email, khachHang.Email),
                    new Claim("MaNguoiDung", khachHang.MaKh.ToString())
                };
                var roles = _context.UserRoles.Where(r => r.UserId == khachHang.MaKh).Select(ur => ur.Role).ToList();
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                }
                var claimIdentity = new ClaimsIdentity(claims, "login");
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);
                await HttpContext.SignInAsync(claimPrincipal);

                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    //nếu là admin
                    if (User.IsInRole("Quản trị Hệ thống"))
                    {
                        return Redirect("/admin/HangHoa");
                    }
                    return RedirectToAction(actionName: "Profile", controllerName: "KhachHang");
                }
            }

            ViewBag.ThongBaoLoi = thongBaoLoi;
            return View();
        }
        #endregion

        [Authorize]
        public IActionResult Profile()
        {
            var user = User.Identities;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("/");
        }

        public async Task<IActionResult> HangDaMua()
        {
            return View();
        }

    }
}