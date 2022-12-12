using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangDienTu.Models.Data;
using WebBanHangDienTu.Models;

namespace WebBanHangDienTu.Controllers
{
    public class MenuController : Controller
    {
        private AppDbContext dbConect = new AppDbContext();

        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var items = dbConect.DanhMucs.OrderBy(x => x.ViTri).ToList();
            return PartialView("_MenuTop", items);
        }
       

        public ActionResult ShowALLSP()
        {
            var items = dbConect.DanhMucSPs.ToList();
            return PartialView("_ShowALLSP", items);
        }
        public ActionResult MenuDanhMucSP()
        {
            var items = dbConect.DanhMucSPs.Take(6).ToList();
            return PartialView("_MenuDanhMucSP", items);
        }
        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.DanhMucId = id;
            }
            var items = dbConect.DanhMucSPs.ToList();
            return PartialView("_MenuLeft", items);
        }
        public ActionResult ShowThuongHieu()
        {
            var items = dbConect.thuongHieus.ToList();

            return PartialView("_ShowThuongHieu", items);
        }
       
    }
}