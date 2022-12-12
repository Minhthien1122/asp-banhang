using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangDienTu.Models.Data;

namespace WebBanHangDienTu.Controllers
{
    public class GioiThieuController : Controller
    {
        private AppDbContext dbConect = new AppDbContext();

        // GET: GioiThieu
        public ActionResult Index()
        {
            var item = dbConect.BaiViets;

            return View(item);
        }
    }
}