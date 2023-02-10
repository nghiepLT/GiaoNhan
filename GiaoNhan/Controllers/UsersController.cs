using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Models;
using DAL;
using BAL;
using Newtonsoft.Json;

namespace GiaoNhan.Controllers
{
    public class UsersController : Controller
    {
        BAL_User user = new BAL_User();
        BAL_Employee employee = new BAL_Employee();
        BAL_Permission permission = new BAL_Permission();
        BAL_Role balRole = new BAL_Role();
        // GET: Users
        public ActionResult GetListUser()
        { 
            ViewBag.ListPermission = permission.GetALl();
            ViewBag.ListRole = balRole.GetAllRole();
            return View();
        }
        public ActionResult GetListUserData(int PermissionID)
        {
            var model = user.GetVMAll(PermissionID);
            return PartialView(model);
        }

        [HttpPost]
        public int UserUpdate(string data)
        {
            tbUSer tbAccount = JsonConvert.DeserializeObject<tbUSer>(data);

            if (tbAccount.UserName!="" && user.CheckUserName(tbAccount.UserName) && tbAccount.UserID==0)
            {
                return 2;
            }
            if (user.UpdateUser(tbAccount))
                return 1; 
            return -1; 
        }

        public JsonResult GetInfoUser(int UserID)
        {
            var model = user.GetbyID(UserID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool DeleteUser(int UserID)
        {
            return user.DeleteUser(UserID);
        }

        public ActionResult ConfigEmail()
        {
            return View();
        }
    }
}