using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangDienTu.Models.Data;
using WebBanHangDienTu.Models;
using WebBanHangDienTu.Filters;

namespace WebBanHangDienTu.Controllers
{
    public class GioHangController : Controller
    {
        private AppDbContext dbConect = new AppDbContext();

        // GET: GioHang
        public ActionResult Index()
        {
            ShopingCart GioHang = (ShopingCart)Session["GioHang"];
            if (GioHang != null && GioHang.items.Any())
            {
                ViewBag.checkGioHang = GioHang;
            }
            return View();
        }

        [AuthenFilter]
        public ActionResult ThanhToan()
        {
            ShopingCart GioHang = (ShopingCart)Session["GioHang"];
            var erroMsg = GioHang.CheckExitItem();
            if (erroMsg != "")
            {
                ViewBag.checkGioHang = erroMsg;
                return View("ThanhToanThatBai");
                
            }
            else
            {
                if (GioHang != null && GioHang.items.Any())
                {
                    ViewBag.checkGioHang = GioHang;
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThanhToan(DonHangModel req)
        {
            var code = new { Success = false, Code = -1 };
            if (ModelState.IsValid)
            {
                ShopingCart GioHang = (ShopingCart)Session["GioHang"];
                if (GioHang != null)
                {
                    DonHang donhang = new DonHang();
                    donhang.TenKH = req.TenKH;
                    donhang.SDT = req.SDT;
                    donhang.DiaChi = req.DiaChi;
                    donhang.Email = req.Email;
                    GioHang.items.ForEach(x => donhang.ChiTietDonHangs.Add(new ChiTietDonHang
                    {
                        SanPhamId = x.SanPhamId,
                        SoLuong = x.SoLuong,
                        DonGia = x.DonGia
                    }));
                    donhang.TongDH = GioHang.items.Sum(x => (x.DonGia * x.SoLuong));
                    donhang.TypePayment = req.TypePayment;
                    donhang.NgayTao = DateTime.Now;
                    donhang.NgaySua = DateTime.Now;
                    donhang.NguoiTao = req.SDT;
                    Random rd = new Random();
                    donhang.MaCodeDH = "DH" + rd.Next(0,9)+ rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    dbConect.DonHangs.Add(donhang);
                    dbConect.SaveChanges();
                    //send mail cho khách hàng //
                    var strSanPham = "";
                    var ThanhTien = decimal.Zero;
                    var TongTien = decimal.Zero;
                    foreach (var sp in GioHang.items)
                    {
                        strSanPham += "<tr>";
                        strSanPham += "<td>"+sp.TenSP +"</td>";
                        strSanPham += "<td>"+sp.SoLuong+"</td>";
                        strSanPham += "<td>"+WebBanHangDienTu.Common.Common.FormatNumber(sp.TongGia,0)+"</td>";
                        strSanPham += "</tr>";
                        ThanhTien += sp.DonGia * sp.SoLuong;
                    }
                    TongTien = ThanhTien;
                    string contenCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/Templates/send2.html"));
                    contenCustomer = contenCustomer.Replace("{{MaDon}}",donhang.MaCodeDH);
                    contenCustomer = contenCustomer.Replace("{{SanPham}}",strSanPham);
                    contenCustomer = contenCustomer.Replace("{{NgayDat}}",DateTime.Now.ToString("dd/MM/yyyy"));
                    contenCustomer = contenCustomer.Replace("{{TenKhachHang}}",donhang.TenKH);
                    contenCustomer = contenCustomer.Replace("{{Phone}}",donhang.SDT);
                    contenCustomer = contenCustomer.Replace("{{Email}}",req.Email);
                    contenCustomer = contenCustomer.Replace("{{DiaChiNhanHang}}", donhang.DiaChi);
                    contenCustomer = contenCustomer.Replace("{{ThanhTien}}", WebBanHangDienTu.Common.Common.FormatNumber(ThanhTien,0));
                    contenCustomer = contenCustomer.Replace("{{TongTien}}", WebBanHangDienTu.Common.Common.FormatNumber(TongTien, 0));
                    WebBanHangDienTu.Common.Common.SendMail("Cửa hàng điện tử", "Đơn hàng #" + donhang.MaCodeDH, contenCustomer, req.Email);

                    string contenAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/Templates/send1.html"));
                    contenAdmin = contenAdmin.Replace("{{MaDon}}", donhang.MaCodeDH);
                    contenAdmin = contenAdmin.Replace("{{SanPham}}", strSanPham);
                    contenAdmin = contenAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contenAdmin = contenAdmin.Replace("{{TenKhachHang}}", donhang.TenKH);
                    contenAdmin = contenAdmin.Replace("{{Phone}}", donhang.SDT);
                    contenAdmin = contenAdmin.Replace("{{Email}}", req.Email);
                    contenAdmin = contenAdmin.Replace("{{DiaChiNhanHang}}", donhang.DiaChi);
                    contenAdmin = contenAdmin.Replace("{{ThanhTien}}", WebBanHangDienTu.Common.Common.FormatNumber(ThanhTien, 0));
                    contenAdmin = contenAdmin.Replace("{{TongTien}}", WebBanHangDienTu.Common.Common.FormatNumber(TongTien, 0));
                    WebBanHangDienTu.Common.Common.SendMailAdmin("Cửa hàng điện tử", "Đơn hàng mới #" + donhang.MaCodeDH, contenAdmin, ConfigurationManager.AppSettings["EmailAdmin"]);
                    GioHang.ClearGioHang();
                    return RedirectToAction("ThanhToanThanhCong");
                }
            }
            return Json(code);
        }
        public ActionResult ThanhToanThanhCong()
        {
            return View();
        }

        public ActionResult Partial_ThanhToan()
        {

            return PartialView();
        }


        public ActionResult PartialItemGioHang()
        {
            ShopingCart GioHang = (ShopingCart)Session["GioHang"];
            if (GioHang != null && GioHang.items.Any())
            {
                return PartialView(GioHang.items);
            }
            return PartialView();
        }

        public ActionResult PartialItemThanhToan()
        {
            ShopingCart GioHang = (ShopingCart)Session["GioHang"];
            if (GioHang != null && GioHang.items.Any())
            {
                return PartialView(GioHang.items);
            }
            return PartialView();
        }

        public ActionResult ShowCount()
        {
            ShopingCart GioHang = (ShopingCart)Session["GioHang"];
            if (GioHang!= null)
            {
                return Json(new { Count=GioHang.items.Count }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { SCount = 0 }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ThemGioHang(int id, int soluong)
        {
            var code = new { success = false, msg = "", code = -1, Count = 0 };
            var db = new AppDbContext();
            var checkSP = db.SanPhams.FirstOrDefault(s => s.Id == id);
            if (checkSP != null)
            {
                ShopingCart GioHang = (ShopingCart)Session["GioHang"];
                if (GioHang == null)
                {
                    GioHang = new ShopingCart();
                }
                ShopingCartItem item = new ShopingCartItem
                {
                    SanPhamId = checkSP.Id,
                    TenSP = checkSP.TenSP,
                    BiDanh = checkSP.BiDanh,
                    TenDanhMucSP = checkSP.DanhMucSP.TenDanhMuc,
                    SoLuong = soluong,
                };
                if (checkSP.HinhAnhSP.FirstOrDefault(x => x.IsDefault) != null)
                {
                    item.HinhSP = checkSP.HinhAnhSP.FirstOrDefault(x => x.IsDefault).HinhAnh;
                }
                item.DonGia = checkSP.DonGia;
                if (checkSP.GiaGiam > 0)
                {
                    item.DonGia = (decimal)checkSP.GiaGiam;
                }
                item.TongGia = item.SoLuong * item.DonGia;
                GioHang.ThemVaoGio(item, soluong);
                Session["GioHang"] = GioHang;
                code = new { success = true, msg = "Thêm sản phẩm vào giỏ hàng thành công!", code = 1, Count = GioHang.items.Count };
            }


            return Json(code);
        }

        [HttpPost]
        public ActionResult Sua(int id, int soluong)
        {
            ShopingCart GioHang = (ShopingCart)Session["GioHang"];
            if (GioHang != null)
            {
                GioHang.UpdateSL(id, soluong);
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }

        [HttpPost]
        public ActionResult Xoa(int id)
        {
            var code = new { success = false, msg = "", code = -1, Count = 0 };
            ShopingCart GioHang = (ShopingCart)Session["GioHang"];
            if (GioHang != null)
            {
                var checkSP = GioHang.items.FirstOrDefault(x => x.SanPhamId == id);
                if (checkSP!=null)
                {
                    GioHang.Xoa(id);
                    code = new { success = true, msg = "", code = 1, Count = GioHang.items.Count };

                }
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShopingCart GioHang = (ShopingCart)Session["GioHang"];
            if (GioHang != null)
            {
                GioHang.ClearGioHang();
                return Json(new {success = true});
            }
            return Json(new { success = false });

        }
        
    }
}