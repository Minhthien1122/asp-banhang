using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;


namespace WebBanHangDienTu.Identity
{
    public class AppUserManeger : UserManager<AppUser>
    {
        public AppUserManeger(IUserStore<AppUser> store): base(store) { }

    }
}