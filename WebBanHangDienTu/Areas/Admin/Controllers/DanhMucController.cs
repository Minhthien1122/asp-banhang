using Microsoft.Ajax.Utilities;
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
    public class DanhMucController : Controller
    {
        private AppDbContext dbConect = new AppDbContext();
        // GET: Admin/DanhMuc
        public ActionResult Index()
        {
            var item = dbConect.DanhMucs;
            return View(item);
        }
        public ActionResult Them()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Them(DanhMuc model)
        {
            if (ModelState.IsValid)
            {
                model.NgayTao = DateTime.Now;
                model.NgaySua = DateTime.Now;
                model.BiDanh = WebBanHangDienTu.Models.Commons.Filter.FilterChar(model.TenDanhMuc);
                dbConect.DanhMucs.Add(model);
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Sua(int id)
        {
            var item = dbConect.DanhMucs.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sua(DanhMuc model)
        {
            if (ModelState.IsValid)
            {
                dbConect.DanhMucs.Attach(model);
                model.NgaySua = DateTime.Now;
                model.BiDanh = WebBanHangDienTu.Models.Commons.Filter.FilterChar(model.TenDanhMuc);
                dbConect.Entry(model).Property(x => x.TenDanhMuc).IsModified = true;
                dbConect.Entry(model).Property(x => x.BiDanh).IsModified = true;
                dbConect.Entry(model).Property(x => x.MoTa).IsModified = true;
                dbConect.Entry(model).Property(x => x.MoTaSEO).IsModified = true;
                dbConect.Entry(model).Property(x => x.TuKhoaSEO).IsModified = true;
                dbConect.Entry(model).Property(x => x.TieuDeSEO).IsModified = true;
                dbConect.Entry(model).Property(x => x.NgaySua).IsModified = true;
                dbConect.Entry(model).Property(x => x.NguoiSua).IsModified = true;
                dbConect.Entry(model).Property(x => x.ViTri).IsModified = true;
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Xoa(int id)
        {
            var item = dbConect.DanhMucs.Find(id);
            if (item != null)
            {
                //var xoaItem = dbConect.DanhMucs.Attach(item);
                dbConect.DanhMucs.Remove(item);
                dbConect.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}