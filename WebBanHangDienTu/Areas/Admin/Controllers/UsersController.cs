using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangDienTu.Filters;
using WebBanHangDienTu.Identity;

namespace WebBanHangDienTu.Areas.Admin.Controllers
{
    [AuthoFilter]
    public class UsersController : Controller
    {
        // GET: Admin/Users
        public ActionResult Index()
        {
            AppDbContextIdentity db = new AppDbContextIdentity();
            List<AppUser> users = db.Users.ToList();
            return View(users);
        }
    }
}