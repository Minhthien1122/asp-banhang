using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangDienTu.Models.Data;
using WebBanHangDienTu.Models;
using WebBanHangDienTu.Filters;

namespace WebBanHangDienTu.Areas.Admin.Controllers
{
    [ManegerAuthorFilter]
    public class BaiVietController : Controller
    {
        private AppDbContext dbConect = new AppDbContext();
        // GET: Admin/BaiViet
        public ActionResult Index(string Searchtext, int? page)
        {
            IEnumerable<BaiViet> items = dbConect.BaiViets.OrderByDescending(x => x.Id);
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
        //[ValidateInput(false)]BaiViets
        public ActionResult Them(BaiViet model)
        {
            if (ModelState.IsValid)
            {
                model.NgayTao = DateTime.Now;
                model.NgaySua = DateTime.Now;
                model.DanhMucId = 2;
                model.BiDanh = WebBanHangDienTu.Models.Commons.Filter.FilterChar(model.TieuDe);
                dbConect.BaiViets.Add(model);
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Sua(int id)
        {
            var item = dbConect.BaiViets.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        public ActionResult Sua(BaiViet model)
        {
            if (ModelState.IsValid)
            {
                model.NgaySua = DateTime.Now;
                model.BiDanh = WebBanHangDienTu.Models.Commons.Filter.FilterChar(model.TieuDe);
                dbConect.BaiViets.Attach(model);
                dbConect.Entry(model).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Xoa(int id)
        {
            var item = dbConect.BaiViets.Find(id);
            if (item != null)
            {
                dbConect.BaiViets.Remove(item);
                dbConect.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public ActionResult IsActive(int id)
        {
            var item = dbConect.BaiViets.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                dbConect.BaiViets.Remove(item);
                dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return Json(new { success = true, item.IsActive });
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
                        var obj = dbConect.BaiViets.Find(Convert.ToInt32(item));
                        dbConect.BaiViets.Remove(obj);
                        dbConect.SaveChanges();
                    }
                }
                return Json(new { success = true });

            }
            return Json(new { success = false });
        }
    }
}