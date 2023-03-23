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
            routes.MapRoute(name: "Quản lý chi phí bảo dưỡng", url: "quan-ly-bao-duong", defaults: new { controller = "Service", action = "ServiceBaoduongIndex" });


            routes.MapRoute(name: "Quản lý bảo trì xe", url: "quan-ly-bao-tri", defaults: new { controller = "Car", action = "HanhTrinhXe" });
            routes.MapRoute(name: "Thống kê chi phí", url: "thong-ke-chi-phi", defaults: new { controller = "Report", action = "ThongKeChiPhi" });
            routes.MapRoute(name: "Thống kê bảo trì", url: "thong-ke-bao-tri", defaults: new { controller = "Report", action = "ThongKeQuangDuong" });

            routes.MapRoute(name: "Sắp xếp giao nhận", url: "sap-xep-giao-nhan", defaults: new { controller = "SapXepGiaoNhan", action = "Sapxeptai" }); 

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
