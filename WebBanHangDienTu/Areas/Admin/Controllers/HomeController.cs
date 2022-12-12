using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBanHangDienTu.Filters;
using WebBanHangDienTu.Identity;
using WebBanHangDienTu.Models;
using WebBanHangDienTu.Models.Data;

namespace WebBanHangDienTu.Areas.Admin.Controllers
{
    [ManegerAuthorFilter]
    public class HomeController : Controller
    {
        private AppDbContext dbConect = new AppDbContext();
        private AppDbContextIdentity db = new AppDbContextIdentity();

        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();//Thống kê tổng doanh thu
            ViewBag.TongDonHang = ThongKeDonhang();//Thống kê tổng đơn hàng
            ViewBag.TongThanhVien = ThongKeThanhVien();//Thống kê tổng thành viên
            //ViewBag.TongDoanhThuThang = ThongKeDoanhThuThang();

            return View();
        }
        public ActionResult ThongKeSP()
        {

            return PartialView();
        }
        public ActionResult Refresh()
        {
            var item = new ThongKeViewModel();
            ViewBag.visitors_online = HttpContext.Application["visitors_online"];
            item.HomNay = HttpContext.Application["HomNay"].ToString();
            item.HomQua = HttpContext.Application["HomQua"].ToString();
            item.TuanNay = HttpContext.Application["TuanNay"].ToString();
            item.TuanTruoc = HttpContext.Application["TuanTruoc"].ToString();
            item.ThangNay = HttpContext.Application["ThangNay"].ToString();
            item.ThangTruoc = HttpContext.Application["ThangTruoc"].ToString();
            item.TatCa = HttpContext.Application["TatCa"].ToString();

            return PartialView(item);
        }
        public decimal ThongKeTongDoanhThu()
        {
            //thống kê tổng doanh thu
            decimal TongDT = decimal.Parse(dbConect.ChiTietDonHangs.Sum(n => (n.SoLuong * n.DonGia)).ToString());

            return TongDT;
        }
        //public decimal ThongKeDoanhThuThang(int thang, int nam)
        //{
        //    //thống kê doanh thu tháng năm
        //    var lstdh = dbConect.DonHangs.Where(n => n.NgayTao.Year == thang && n.NgayTao.Year == nam);
        //    decimal tongTien = 0;
        //    foreach (var item in lstdh)
        //    {
        //        tongTien += decimal.Parse(item.ChiTietDonHangs.Sum(n => (n.SoLuong * n.DonGia)).ToString());
        //    }

        //    return tongTien;
        //}

        public double ThongKeDonhang()
        {
            //Đem đơn hàng
            double DonHang = dbConect.DonHangs.Count();
            return DonHang;
        }
        public double ThongKeThanhVien()
        {
            //Đem đơn hàng
            double thanhVien = db.Users.Where(x => (x.UserName != "admin" && x.UserName != "Maneger")).Count();

            return thanhVien;
        }

        [HttpPost]
        public ActionResult ThongKeSanPhanTheoType(string type, DateTime fromDate, DateTime fromTo)
        {
            List<ThongkeSPModel> query = new List<ThongkeSPModel>();
            switch (type)
            {
                case "Ngay":
                    query = (from s in dbConect.DonHangs
                             join c in dbConect.ChiTietDonHangs on s.Id equals c.DonHangId
                             join sp in dbConect.SanPhams on c.SanPhamId equals sp.Id
                             where s.NgayTao >= fromDate && s.NgayTao <= fromTo
                             group s by s.NgayTao.Date into day
                             select new ThongkeSPModel
                             {
                                 ngayTao = day.Key,
                                 TongDh = day.Sum(x => x.TongDH),
                                 //GiaNhap = .Sum(x=>x.Gia)
                             }).OrderBy(x => x.ngayTao).ToList();
                    break;
                case "Thang":
                    break;
                case "Nam":
                    break;
                default:
                    break;

            }

            ViewBag.abc = type;
            return View("Index");
        }


      
    }
}