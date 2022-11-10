using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Models;
using DAL;
using BAL;
namespace GiaoNhan.Controllers
{
    public class EmployeeController : Controller
    {
        DAL_Permission permission = new DAL_Permission();
        DAL_Employee employee = new DAL_Employee();
        // GET: Employee
        public ActionResult EmployeeList()
        {
            ViewBag.ListPermission = permission.GetALl();
            var model = employee.GetAllVM();
            return View(model);
        }

        [HttpPost]
        public bool EmployeeUpdate(int  EmployeeID,string EmployeeName,int PermissionID)
        {
            return employee.EmployeeUpdate(EmployeeID, EmployeeName, PermissionID);
             
        }

        public JsonResult GetInfoEmployee(int EmployeeID)
        {
            var model = employee.GetById(EmployeeID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}