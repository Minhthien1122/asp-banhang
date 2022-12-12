using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebBanHangDienTu.Models;
using System.Web.Helpers;
using WebBanHangDienTu.Identity;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace WebBanHangDienTu.Controllers
{
    //    [Authorize]
    public class AccountController : Controller
    {

        //
        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        ////
        //// POST: /Account/Login
        [HttpPost]
        public ActionResult Login(DangNhapModel model)
        {
            var appDbContext = new AppDbContextIdentity();
            var userStore = new AppUserStore(appDbContext);
            var userManeger = new AppUserManeger(userStore);
            var user =userManeger.Find(model.UserName, model.Password);
            if (user!=null)
            {
                var authenManeger = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManeger.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenManeger.SignIn(new AuthenticationProperties(), userIdentity);
                if (userManeger.IsInRole(user.Id, "Admin")|| userManeger.IsInRole(user.Id, "Maneger"))
                {
                    return RedirectToAction ("Index", "Home",new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");

                }
            }
            else
            {
                ModelState.AddModelError("My Error", "Invalid username and password");
                return View();
            }
            
        }
        public ActionResult Logout()
        {
            var authenManeger = HttpContext.GetOwinContext().Authentication;
            authenManeger.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }


        //POST: /Account/Register
      
       [HttpPost]
        public ActionResult Register(DangKyModel model)
        {
            if (ModelState.IsValid)
            {
                var appDbContext = new AppDbContextIdentity();
                var userStore = new AppUserStore(appDbContext);
                var userManeger = new AppUserManeger(userStore);
                var passwdHash = Crypto.HashPassword(model.Password);
                var user = new AppUser()
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    PasswordHash = passwdHash,
                    City = model.City,
                    NgaySinh = model.NgaySinh,
                    Address = model.Address,
                    PhoneNumber = model.Phone
                };
                IdentityResult identityResult = userManeger.Create(user);
                if (identityResult.Succeeded)
                {
                    userManeger.AddToRole(user.Id, "Customer");
                    var authenManeger = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManeger.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenManeger.SignIn(new AuthenticationProperties(), userIdentity);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("New Error", "Data Invalid");
                return View();
            }
        }

        

        
    }
}