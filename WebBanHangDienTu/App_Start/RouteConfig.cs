using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebBanHangDienTu
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "GioHang",
               url: "gio-hang",
               defaults: new { controller = "GioHang", action = "Index", BiDanh = UrlParameter.Optional },
               new[] { "WebBanHangDienTu.Controllers" }
           );
            routes.MapRoute(
              name: "ThanhToan",
              url: "thanh-toan",
              defaults: new { controller = "GioHang", action = "ThanhToan", BiDanh = UrlParameter.Optional },
              new[] { "WebBanHangDienTu.Controllers" }
          );
            routes.MapRoute(
               name: "GioiThieu",
               url: "gioi-thieu",
               defaults: new { controller = "GioiThieu", action = "Index", BiDanh = UrlParameter.Optional },
               new[] { "WebBanHangDienTu.Controllers" }
           );
            routes.MapRoute(
               name: "LienHe",
               url: "lien-he",
               defaults: new { controller = "LienHe", action = "Index", BiDanh = UrlParameter.Optional },
               new[] { "WebBanHangDienTu.Controllers" }
           );
            routes.MapRoute(
               name: "TinTuc",
               url: "tin-tuc",
               defaults: new { controller = "TinTuc", action = "Index", BiDanh = UrlParameter.Optional },
               new[] { "WebBanHangDienTu.Controllers" }
           );
            routes.MapRoute(
                name: "DanhMucSP",
                url: "danh-muc-san-pham/{BiDanh}-{id}",
                defaults: new { controller = "SanPham", action = "DanhMucSP", Id = UrlParameter.Optional },
                new[] { "WebBanHangDienTu.Controllers" }
            );
            routes.MapRoute(
                name: "ChiTietSanPham",
                url: "chi-tiet/{BiDanh}-{id}",
                defaults: new { controller = "SanPham", action = "ChiTietSP", BiDanh = UrlParameter.Optional },
                new[] { "WebBanHangDienTu.Controllers" }
            );
            routes.MapRoute(
                name: "ChiTietTinTuc",
                url: "tin-tuc/{BiDanh}-{id}",
                defaults: new { controller = "TinTuc", action = "ChiTietTinTuc", BiDanh = UrlParameter.Optional },
                new[] { "WebBanHangDienTu.Controllers" }
            );
            routes.MapRoute(
                name: "SanPham",
                url: "san-pham",
                defaults: new { controller = "SanPham", action = "Index", BiDanh = UrlParameter.Optional },
                new[] { "WebBanHangDienTu.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "WebBanHangDienTu.Controllers" }
            );

        }
    }
}
