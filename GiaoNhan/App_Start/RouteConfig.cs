using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BAL;
namespace GiaoNhan
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            BAL_Permission permission = new BAL_Permission();
            var getListRoute=permission.GetALlMenu();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            foreach(var item in getListRoute)
            { 
                routes.MapRoute(name:item.MenuName, url: item.MenuURL, defaults: new { controller = item.Controller, action = item.Action });
            }
            routes.MapRoute(name: "Quản lý loại xe", url: "quan-ly-loai-xe", defaults: new { controller = "Car", action = "TypeCarIndex" });

            routes.MapRoute(name: "Quản lý xe", url: "quan-ly-xe", defaults: new { controller = "Car", action = "CarIndex" });
            routes.MapRoute(name: "Quản lý tài xế", url: "quan-ly-tai-xe", defaults: new { controller = "Car", action = "TaixeIndex" });
            routes.MapRoute(name: "Quản lý chi phí", url: "quan-ly-chi-phi", defaults: new { controller = "Service", action = "ServiceIndex" });
            routes.MapRoute(name: "Quản lý quãng đường xe", url: "quan-ly-quang-duong-xe", defaults: new { controller = "Car", action = "HanhTrinhXe" });

            routes.MapRoute(name: "Đăng nhập", url: "dang-nhap", defaults: new { controller = "Login", action = "Index" }); 

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
