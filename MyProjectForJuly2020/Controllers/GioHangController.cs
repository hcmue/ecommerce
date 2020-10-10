using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyProjectForJuly2020.Data;
using MyProjectForJuly2020.Helpers;
using MyProjectForJuly2020.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PayPal.Core;
using PayPal.v1.Payments;
using BraintreeHttp;

namespace MyProjectForJuly2020.Controllers
{
    public class GioHangController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _clientId;
        private readonly string _secretKey;

        public double TyGiaUSD = 23300;//store in Database
        public GioHangController(MyDbContext ctx, IMapper mapper, IConfiguration config)
        {
            _context = ctx;
            _mapper = mapper;
            _clientId = config["PaypalSettings:ClientId"];
            _secretKey = config["PaypalSettings:SecretKey"];
        }

        public List<CartItem> Carts
        {
            get
            {
                var carts = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (carts == null)
                {
                    carts = new List<CartItem>();
                }
                return carts;
            }
        }

        public IActionResult Index()
        {
            return View(Carts);
        }

        public IActionResult ThemVaoGio(Guid id, string addType, int qty = 1)
        {
            //lấy giỏ hàng hiện tại
            var myCart = Carts;

            //kiểm tra hàng đã có trong giỏ
            var item = myCart.SingleOrDefault(it => it.MaHangHoa == id);
            if (item != null)//đã có
            {
                item.SoLuong += qty;
            }
            else
            {
                var hh = _context.HangHoas.FirstOrDefault(p => p.MaHangHoa == id);
                item = _mapper.Map<CartItem>(hh);
                item.SoLuong = qty;
                myCart.Add(item);
            }
            HttpContext.Session.Set("GioHang", myCart);

            if (addType == "ajax")
                return PartialView("_CartView");

            return RedirectToAction("Index");
        }

        public IActionResult RemoveCartItem(Guid id, bool isAjaxCall = false)
        {
            //lấy giỏ hàng hiện tại
            var myCart = Carts;

            //kiểm tra hàng đã có trong giỏ
            var item = myCart.SingleOrDefault(it => it.MaHangHoa == id);
            if (item != null)
            {
                myCart.Remove(item);
                HttpContext.Session.Set("GioHang", myCart);
            }

            if (isAjaxCall) { }

            return RedirectToAction("Index");
        }

        [Authorize, HttpGet]
        public IActionResult ThanhToan()
        {
            return View();
        }

        [Authorize, HttpPost]
        public IActionResult ThanhToan(ThanhToanVM model)
        {
            if (ModelState.IsValid)
            {
                var emailKh = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                var maKH = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "MaNguoiDung").Value);
                using (var trans = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var donHang = new DonHang
                        {
                            MaDh = Guid.NewGuid(),
                            MaKh = maKH,
                            NgayDat = DateTime.UtcNow,
                            TinhTrangDonHang = TinhTrangDonHang.MoiDatHang,
                            DiaChiGiao = model.DiaChiGiao,
                            NguoiNhan = model.NguoiNhan
                        };
                        _context.Add(donHang);
                        foreach (var item in Carts)
                        {
                            _context.Add(new DonHangChiTiet
                            {
                                MaDh = donHang.MaDh,
                                MaHh = item.MaHangHoa,
                                SoLuong = item.SoLuong,
                                DonGia = item.DonGia
                            });
                        }
                        _context.SaveChanges();
                        trans.Commit();
                        HttpContext.Session.Remove("GioHang");
                        return Redirect("/KhachHang/HangDaMua");
                    }
                    catch (Exception ex)
                    {
                        //log
                        trans.Rollback();
                        return View();
                    }
                }

            }
            return View();
        }

        [Authorize]
        public async System.Threading.Tasks.Task<IActionResult> PaypalCheckout()
        {
            var environment = new SandboxEnvironment(_clientId, _secretKey);
            var client = new PayPalHttpClient(environment);

            #region Create Paypal Order
            var itemList = new ItemList()
            {
                Items = new List<Item>()
            };
            var total = Math.Round(Carts.Sum(p => p.ThanhTien) / TyGiaUSD, 2);
            foreach (var item in Carts)
            {
                itemList.Items.Add(new Item()
                {
                    Name = item.TenHh,
                    Currency = "USD",
                    Price = Math.Round(item.DonGia / TyGiaUSD, 2).ToString(),
                    Quantity = item.SoLuong.ToString(),
                    Sku = "sku",
                    Tax = "0"
                });
            }
            #endregion

            var paypalOrderId = DateTime.Now.Ticks;
            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = total.ToString(),
                            Currency = "USD",
                            Details = new AmountDetails
                            {
                                Tax = "0",
                                Shipping = "0",
                                Subtotal = total.ToString()
                            }
                        },
                        ItemList = itemList,
                        Description = $"Invoice #{paypalOrderId}",
                        InvoiceNumber = paypalOrderId.ToString()
                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    CancelUrl = $"{hostname}/GioHang/CheckoutFail",
                    ReturnUrl = $"{hostname}/GioHang/CheckoutSuccess"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };

            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);

            try
            {
                var response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment  
                        paypalRedirectUrl = lnk.Href;
                    }
                }

                return Redirect(paypalRedirectUrl);
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                //Process when Checkout with Paypal fails
                return Redirect("/GioHang/CheckoutFail");
            }
        }

        public IActionResult CheckoutFail()
        {
            //Tạo đơn hàng trong database với trạng thái thanh toán là "Chưa thanh toán"
            //Xóa session
            return View();
        }
        
        public IActionResult CheckoutSuccess()
        {
            //Tạo đơn hàng trong database với trạng thái thanh toán là "Paypal" và thành công
            //Xóa session
            return View();
        }
    }
}