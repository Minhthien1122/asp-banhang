using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangDienTu.Models.Data;
using WebBanHangDienTu.Models;
using PagedList;

namespace WebBanHangDienTu.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext dbConect = new AppDbContext();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Partial_Subcrice()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subscrice(DangKies req)
        {
            if (ModelState.IsValid)
            {
                dbConect.DangKies.Add(new DangKies { Email = req.Email, NgayTao = DateTime.Now });
                dbConect.SaveChanges();
                return Json(true);
            }
            return View("Partial_Subcrice"); }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
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

        public ActionResult TimKiem(string Searchtext, int? page)
        {

            IEnumerable<SanPham> items = dbConect.SanPhams.OrderByDescending(x => x.Id);
            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(x => x.BiDanh.Contains(Searchtext) || x.TenSP.Contains(Searchtext)|| x.TieuDeSEO.Contains(Searchtext)||x.MoTaSEO.Contains(Searchtext)|| x.TuKhoaSEO.Contains(Searchtext));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.Searchtext = Searchtext;
            return View(items);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}