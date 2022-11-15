using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using DAL.Models;
namespace GiaoNhan.Controllers
{
    public class RoleController : Controller
    {
        BAL_Permission balPermission = new BAL_Permission();
        BAL_Role balRole = new BAL_Role();
        // GET: Role
        public ActionResult RoleList()
        {
            var model = balRole.GetAllRole();
            ViewBag.Listmenu = balPermission.GetALlMenu();
            return View(model);
        } 
        public bool UpdateRole(int RoleId,string RoleName,List<int> lstMenu)
        {
            tbRole tbRole = new tbRole();
            tbRole.RoleId = RoleId;
            tbRole.RoleName = RoleName;
            balRole.InsertRole(tbRole, lstMenu);
            return true;
        }
        public JsonResult GetInfoRole(int RoleId)
        {
            var model = balRole.GetRole(RoleId);
            string str = "";
            foreach(var item in model.lsttbMenu)
            {

                str += item.MenuId + ",";
            }
            ViewBag.ListMenu = str;
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public bool DeleteRole(int RoleId)
        {
            return balRole.DeleteRole(RoleId);
        }
         
       
    }
}