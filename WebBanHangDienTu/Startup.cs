using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Cookies;
using WebBanHangDienTu.Identity;

[assembly: OwinStartup(typeof(WebBanHangDienTu.Startup))]

namespace WebBanHangDienTu
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType =
                DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            }); 
            this.CreateRolesAndUser();
        }

        public void CreateRolesAndUser()
        {
            var roleManeger = new RoleManager<IdentityRole>(new 
                RoleStore<IdentityRole>(new AppDbContextIdentity()));
            var appDbContext = new AppDbContextIdentity();
            var appUserStore = new AppUserStore(appDbContext);
            var userManeger = new AppUserManeger(appUserStore);

            if (!roleManeger.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManeger.Create(role);
            }
            if (userManeger.FindByName("admin") == null)
            {
                var user = new AppUser();
                user.UserName = "admin";
                user.Email = "admin@gmail.com";
                string userPwd = "Admin123";
                var checkUser = userManeger.Create(user, userPwd);
                if (checkUser.Succeeded)
                {
                    userManeger.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManeger.RoleExists("Maneger"))
            {
                var role = new IdentityRole();
                role.Name = "Maneger";
                roleManeger.Create(role);
            }
            if (userManeger.FindByName("User") == null)
            {
                var user = new AppUser();
                user.UserName = "Maneger";
                user.Email = "Maneger@gmail.com";
                string userPwd = "Maneger123";
                var checkUser = userManeger.Create(user, userPwd);
                if (checkUser.Succeeded)
                {
                    userManeger.AddToRole(user.Id, "Maneger");
                }
            }

            if (!roleManeger.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManeger.Create(role);
            }

        }
    }
}
