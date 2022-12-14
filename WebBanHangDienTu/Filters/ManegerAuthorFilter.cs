using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHangDienTu.Filters
{
    public class ManegerAuthorFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if ((filterContext.HttpContext.User.IsInRole("Maneger")|| filterContext.HttpContext.User.IsInRole("Admin")) == false)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}

