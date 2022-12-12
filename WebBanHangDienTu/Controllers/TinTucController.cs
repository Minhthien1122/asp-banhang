using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangDienTu.Models.Data;
using WebBanHangDienTu.Models;

namespace WebBanHangDienTu.Controllers
{
    public class TinTucController : Controller
    {
        private AppDbContext dbConect = new AppDbContext();

        // GET: TinTuc
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Partial_TinTuc() 
        {
            var items = dbConect.BanTins.Where(x => x.IsActive).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_TinTuc_Home()
        {
            var items = dbConect.BanTins.Where(x => x.IsActive).Take(3).ToList();
            return PartialView(items);
        }

        public ActionResult ChiTietTinTuc(int id)
        {
            var item = dbConect.BanTins.Find(id);
           
            return View(item);
        }
    }
}
