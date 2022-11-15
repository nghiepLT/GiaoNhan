using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Office.Interop;
using System.Data;
using System.Data.OleDb;
using BAL;
using DAL.Models;
//using GiaoNhan.Models;
namespace GiaoNhan.Controllers
{
    public class HomeController : Controller
    {
        BAL_User user = new BAL_User();
        BAL_Permission permission = new BAL_Permission();
        BAL_Role balrole = new BAL_Role();
        //KpiManagerEntities dbcontext = new KpiManagerEntities();
        public ActionResult Index()
        {
            string returnMenu = "/";
            if (Request.Cookies["trakinglogin"] != null)
            {
                var userName = Request.Cookies["trakinglogin"].Value;
                tbUSer tbaccount = user.GetAccountID(userName);
                ViewBag.Menu = balrole.GetLeftMenu(tbaccount.UserID).OrderByDescending(m=>m.STT).FirstOrDefault().MenuURL;
                returnMenu = ViewBag.Menu;
                return Redirect(returnMenu);
            }
            return Redirect("/login");
        }
      
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SlideBarMenu()
        {
            if (Request.Cookies["trakinglogin"] != null)
            {
                var userName = Request.Cookies["trakinglogin"].Value;
                tbUSer tbaccount = user.GetAccountID(userName);
                ViewBag.ListMN = balrole.GetLeftMenu(tbaccount.UserID);
               // ViewBag.ListMenu = permission.GetMenuByPermission(tbaccount.PermissionID);
                ViewBag.AccountUser = tbaccount.UserName;
            }
            return PartialView();
        }
        public ActionResult Rightnavbar()
        {
            if (Request.Cookies["trakinglogin"] != null)
            {
                var userName = Request.Cookies["trakinglogin"].Value;
                tbUSer tbaccount = user.GetAccountID(userName);
                ViewBag.AccountUser = tbaccount.UserName;
            }

            return PartialView();
        }
    }
}