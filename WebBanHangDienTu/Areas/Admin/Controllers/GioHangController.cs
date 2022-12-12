using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebBanHangDienTu.Models.Data;
using WebBanHangDienTu.Models;
using WebBanHangDienTu.Filters;

namespace WebBanHangDienTu.Areas.Admin.Controllers
{
    [ManegerAuthorFilter]
    public class GioHangController : Controller
    {
        private AppDbContext dbConect = new AppDbContext();

        // GET: Admin/GioHang
        public ActionResult Index(int? page, string Searchtext)
        {
            IEnumerable<DonHang> items = dbConect.DonHangs.OrderByDescending(x => x.Id);
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(x => x.MaCodeDH.Contains(Searchtext) || x.TenKH.Contains(Searchtext) || x.SDT.Contains(Searchtext));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.Searchtext = Searchtext;

            return View(items);
        }
        public ActionResult View(int id)
        {
            var item = dbConect.DonHangs.Find(id);
            return View(item);
        }
        public ActionResult Partial_SP(int id)
        {
            var items = dbConect.ChiTietDonHangs.Where(x=>x.DonHangId==id).ToList();
            return PartialView(items);
        }

        [HttpPost]
        public ActionResult UpdateTT(int id, int TrangThai)
        {
            var item = dbConect.DonHangs.Find(id);
            if (item!= null)
            {
                dbConect.DonHangs.Attach(item);
                item.TypePayment = TrangThai;
                dbConect.Entry(item).Property(x => x.TypePayment).IsModified = true;
                dbConect.SaveChanges();
                return Json(new { Message = "success", success = true });
            }
            return Json(new { Message = "Unsuccess", success = false });

        }
    }
}