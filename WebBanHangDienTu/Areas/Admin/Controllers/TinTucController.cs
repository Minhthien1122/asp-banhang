using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBanHangDienTu.Models.Data;
using WebBanHangDienTu.Models;
using WebBanHangDienTu.Filters;

namespace WebBanHangDienTu.Areas.Admin.Controllers
{
    [ManegerAuthorFilter]

    public class TinTucController : Controller
    {
        private AppDbContext dbConect = new AppDbContext();
        // GET: Admin/TinTuc
        public ActionResult Index(string Searchtext, int? page)
        {
            IEnumerable<BanTin> items = dbConect.BanTins.OrderByDescending(x => x.Id);
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(x => x.BiDanh.Contains(Searchtext) || x.TieuDe.Contains(Searchtext));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.Searchtext = Searchtext;

            return View(items);
        }
        public ActionResult Them()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        public ActionResult Them(BanTin model)
        {
            if (ModelState.IsValid)
            {
                model.NgayTao = DateTime.Now;
                model.NgaySua = DateTime.Now;
                model.DanhMucId = 3;
                model.BiDanh = WebBanHangDienTu.Models.Commons.Filter.FilterChar(model.TieuDe);
                dbConect.BanTins.Add(model);
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Sua(int id)
        {
            var item = dbConect.BanTins.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        public ActionResult Sua(BanTin model)
        {
            if (ModelState.IsValid)
            {
                model.NgaySua = DateTime.Now;
                model.BiDanh = WebBanHangDienTu.Models.Commons.Filter.FilterChar(model.TieuDe);
                dbConect.BanTins.Attach(model);
                dbConect.Entry(model).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Xoa(int id)
        {
            var item = dbConect.BanTins.Find(id);
            if (item != null)
            {
                dbConect.BanTins.Remove(item);
                dbConect.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public ActionResult IsActive(int id)
        {
            var item = dbConect.BanTins.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                dbConect.BanTins.Remove(item);
                dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return Json(new { success = true, IsActive = item.IsActive });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult XoaAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = dbConect.BanTins.Find(Convert.ToInt32(item));
                        dbConect.BanTins.Remove(obj);
                        dbConect.SaveChanges();
                    }
                }
                return Json(new { success = true });

            }
            return Json(new { success = false });
        }
    }
}