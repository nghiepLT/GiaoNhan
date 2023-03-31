using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
namespace GiaoNhan.Controllers
{
    public class LoginController : Controller
    {
        BAL_User account = new BAL_User();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public bool Login(string userName, string passWord)
        {
            var chkLogin = account.Login(userName, passWord);
            if (chkLogin)
            {
                HttpCookie cookie = new HttpCookie("trakinglogin");
                cookie.Value = userName;
                if(userName== "linhnt")
                {
                    cookie.Expires = DateTime.Now.AddDays(1000);
                }
                else
                {
                    cookie.Expires = DateTime.Now.AddDays(1);
                }
                Response.Cookies.Add(cookie);
                return true;
            }
            return false;
        }
        public void Logout()
        {
            var cokie = Request.Cookies["trakinglogin"];
            cokie.Expires = DateTime.Now.AddDays(-1);
            cokie.Value = null;
            Response.Cookies.Add(cokie);
        }
        public int ChangePassword(string UserName, string oldPassword, string newPassword)
        {
            return account.ChangePassword(UserName,oldPassword,newPassword);
        }
    }
}