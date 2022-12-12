using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using System.Web.UI;
using WebBanHangDienTu.Filters;
using WebBanHangDienTu.Models.Data;

namespace WebBanHangDienTu.Areas.Admin.Controllers
{
    [ManegerAuthorFilter]
    public class DanhMucSanPhamController : Controller
    {
        private AppDbContext dbConect = new AppDbContext();

        // GET: Admin/DanhMucSanPham
        public ActionResult Index(string Searchtext, int? page)
        {
            IEnumerable<DanhMucSP> items = dbConect.DanhMucSPs.OrderByDescending(x => x.Id);
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(x => x.BiDanh.Contains(Searchtext) || x.TenDanhMuc.Contains(Searchtext));
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
        public ActionResult Them(DanhMucSP model)
        {
            if (ModelState.IsValid)
            {
                model.NgayTao = DateTime.Now;
                model.NgaySua = DateTime.Now;
                model.BiDanh = WebBanHangDienTu.Models.Commons.Filter.FilterChar(model.TenDanhMuc);
                dbConect.DanhMucSPs.Add(model);
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Sua(int id)
        {
            var item = dbConect.DanhMucSPs.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        public ActionResult Sua(DanhMucSP model)
        {
            if (ModelState.IsValid)
            {
                model.NgaySua = DateTime.Now;
                model.BiDanh = WebBanHangDienTu.Models.Commons.Filter.FilterChar(model.TenDanhMuc);
                dbConect.DanhMucSPs.Attach(model);
                dbConect.Entry(model).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Xoa(int id)
        {
            var item = dbConect.DanhMucSPs.Find(id);
            if (item != null)
            {
                dbConect.DanhMucSPs.Remove(item);
                dbConect.SaveChanges();
                return Json(new { success = true });
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
                        var obj = dbConect.DanhMucSPs.Find(Convert.ToInt32(item));
                        dbConect.DanhMucSPs.Remove(obj);
                        dbConect.SaveChanges();
                    }
                }
                return Json(new { success = true });

            }
            return Json(new { success = false });
        }
    }
}