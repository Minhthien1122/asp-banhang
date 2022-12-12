using CKFinder.Settings;
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
    public class HinhAnhSPController : Controller
    {
        private AppDbContext dbConect = new AppDbContext();

        // GET: Admin/HinhAnhSP
        public ActionResult Index(int id)
        {
            ViewBag.SanPhamId = id;
            var items = dbConect.HinhAnhSPs.Where(x => x.SanPhamId == id).ToList();
            return View(items);
        }

        [HttpPost]
        public ActionResult ThemHinh(int SanPhamId, string url)
        {
            dbConect.HinhAnhSPs.Add(new HinhAnhSP
            {
                SanPhamId=SanPhamId,
                HinhAnh = url,
                IsDefault = false
            });
            
            dbConect.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Xoa(int id)
        {
            var item = dbConect.HinhAnhSPs.Find(id);
            dbConect.HinhAnhSPs.Remove(item);
            dbConect.SaveChanges();
            return Json(new { success = true });
        }

        public ActionResult IsDefault(int id)
        {
            var item = dbConect.HinhAnhSPs.Find(id);
            if (item != null)
            {

                item.IsDefault = !item.IsDefault;
                
                dbConect.HinhAnhSPs.Remove(item);
                dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return Json(new { success = true, item.IsDefault });
            }
            return Json(new { success = false });
        }

    }
}