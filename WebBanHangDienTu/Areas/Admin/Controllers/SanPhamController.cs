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
    public class SanPhamController : Controller
    {
         private AppDbContext dbConect = new AppDbContext();
        // GET: Admin/SanPham
        public ActionResult Index( int? page, string Searchtext)
        {

            IEnumerable<SanPham> items = dbConect.SanPhams.OrderByDescending(x => x.Id);
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(x => x.BiDanh.Contains(Searchtext) || x.TenSP.Contains(Searchtext));
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
            ViewBag.DanhMucSanPham = new SelectList(dbConect.DanhMucSPs.ToList(), "Id", "TenDanhMuc");
            ViewBag.ThuongHieu = new SelectList(dbConect.thuongHieus.ToList(), "Id", "TenTH");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        public ActionResult Them(SanPham model, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                if (Images!=null && Images.Count>0)
                {
                    for(int i=0; i < Images.Count; i++)
                    {
                        if (i + 1 == rDefault[0])
                        {
                            model.HinhAnh = Images[i];
                            model.HinhAnhSP.Add(new HinhAnhSP
                            {
                                SanPhamId = model.Id,
                                HinhAnh = Images[i],
                                IsDefault=true
                            });
                        }
                        else
                        {
                            model.HinhAnhSP.Add(new HinhAnhSP
                            {
                                SanPhamId = model.Id,
                                HinhAnh = Images[i],
                                IsDefault = false
                            });
                        }
                    }
                }
               
                model.NgayTao = DateTime.Now;
                model.NgaySua = DateTime.Now;
               
                if (string.IsNullOrEmpty(model.TieuDeSEO))
                {
                    model.TieuDeSEO = model.TenSP;
                }
                if (string.IsNullOrEmpty(model.BiDanh))
                {
                    model.BiDanh = WebBanHangDienTu.Models.Commons.Filter.FilterChar(model.TenSP);
                }
                dbConect.SanPhams.Add(model);
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DanhMucSanPham = new SelectList(dbConect.DanhMucSPs.ToList(), "Id", "TenDanhMuc");
            ViewBag.ThuongHieu = new SelectList(dbConect.thuongHieus.ToList(), "Id", "TenTH");

            return View(model);
        }

        public ActionResult Sua(int id)
        {
            ViewBag.DanhMucSanPham = new SelectList(dbConect.DanhMucSPs.ToList(), "Id", "TenDanhMuc");
            ViewBag.ThuongHieu = new SelectList(dbConect.thuongHieus.ToList(), "Id", "TenTH");

            var item = dbConect.SanPhams.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sua(SanPham model, int id)
        {
            if (ModelState.IsValid)
            {
                model.HinhAnh = dbConect.HinhAnhSPs.FirstOrDefault(x =>x.IsDefault == true && x.SanPhamId == id ).HinhAnh;
                model.NgaySua = DateTime.Now;
                model.BiDanh = WebBanHangDienTu.Models.Commons.Filter.FilterChar(model.TenSP);
                dbConect.SanPhams.Attach(model);
                dbConect.Entry(model).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Xoa(int id)
        {
            var item = dbConect.SanPhams.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.IsActive = false;
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
                        var obj = dbConect.SanPhams.Find(Convert.ToInt32(item));
                        dbConect.SanPhams.Remove(obj);
                        dbConect.SaveChanges();
                    }
                }
                return Json(new { success = true });

            }
            return Json(new { success = false });
        }
        public ActionResult IsActive(int id)
        {
            var item = dbConect.SanPhams.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                dbConect.SanPhams.Remove(item);
                dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return Json(new { success = true, item.IsActive });
            }
            return Json(new { success = false });
        }
        //public ActionResult IsHome(int id)
        //{
        //    var item = dbConect.SanPhams.Find(id);
        //    if (item != null)
        //    {
        //        item.IsHome = !item.IsHome;
        //        dbConect.SanPhams.Remove(item);
        //        dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
        //        dbConect.SaveChanges();
        //        return Json(new { success = true, item.IsHome });
        //    }
        //    return Json(new { success = false });
        //}
        public ActionResult IsHot(int id)
        {
            var item = dbConect.SanPhams.Find(id);
            if (item != null)
            {
                item.IsHot = !item.IsHot;
                dbConect.SanPhams.Remove(item);
                dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return Json(new { success = true, item.IsHot });
            }
            return Json(new { success = false });
        }
        public ActionResult IsSale(int id)
        {
            var item = dbConect.SanPhams.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                dbConect.SanPhams.Remove(item);
                dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return Json(new { success = true, item.IsSale });
            }
            return Json(new { success = false });
        }
        public ActionResult IsFeature(int id)
        {
            var item = dbConect.SanPhams.Find(id);
            if (item != null)
            {
                item.IsFeature = !item.IsFeature;
                dbConect.SanPhams.Remove(item);
                dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return Json(new { success = true, item.IsFeature });
            }
            return Json(new { success = false });
        }
    }
}