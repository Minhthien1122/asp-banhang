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
    public class SanPhamController : Controller
    {
        private AppDbContext dbConect = new AppDbContext();

        // GET: SanPham
        public ActionResult Index(string Searchtext, int? page)
        {
                IEnumerable<SanPham> items = dbConect.SanPhams.Where(x => x.IsActive).OrderByDescending(x => x.Id);
                var pageSize = 12;
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
        public ActionResult ChiTietSP(int id)
        {
            var item = dbConect.SanPhams.Find(id);
            //Hiển thị lượt xem sản phẩm
            if(item!= null)
            {
                dbConect.SanPhams.Attach(item);
                item.LuotXem = item.LuotXem + 1;
                dbConect.Entry(item).Property(x=>x.LuotXem).IsModified = true;
                dbConect.SaveChanges();
            }

            return View(item);
        }
        public ActionResult DanhMucSP(string Searchtext, int? page, int id)
        {
            IEnumerable<SanPham> items = dbConect.SanPhams.OrderByDescending(x => x.Id);
            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(x => x.BiDanh.Contains(Searchtext) || x.TenSP.Contains(Searchtext));
            }
           
            if (id > 0)
            {
                items = items.Where(x => x.DanhMucSPId == id).ToList();
            }
            var DanhMuc = dbConect.DanhMucSPs.Find(id);
            if(DanhMuc != null)
            {
                ViewBag.TenDM = DanhMuc.TenDanhMuc;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.Searchtext = Searchtext;
            ViewBag.DanhMucId = id;
            return View(items);
        }
        public ActionResult SanPhamTheoDanhMuc()
        {
            var items = dbConect.SanPhams.Where(x => x.IsActive).Take(10).ToList();
            return PartialView(items);
        }
        public ActionResult SanPhamTheoThuongHieu()
        {
            var items = dbConect.SanPhams.Where(x => x.IsActive).Take(10).ToList();
            return PartialView(items);
        }
        public ActionResult SanPhamSale()
        {
            var items = dbConect.SanPhams.Where(x => ((x.IsActive) && (x.IsSale))).ToList();
            return PartialView(items);
        }
        public ActionResult SanPhamHot()
        {
            var items = dbConect.SanPhams.Where(x => ((x.IsActive) && (x.IsHot))).ToList();
            return PartialView(items);
        }
        public ActionResult SanPhamNoiBat()
        {
            var items = dbConect.SanPhams.Where(x => ((x.IsActive) && (x.IsFeature))).ToList();
            return PartialView(items);
        }
    }
}