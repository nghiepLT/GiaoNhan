using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using Newtonsoft.Json;
using DAL.Models;
namespace GiaoNhan.Controllers
{
    public class ServiceController : Controller
    {
        BAL_Service balService = new BAL_Service();
        BAL_Car balCar = new BAL_Car();
        // GET: Service
        
        public ActionResult ServiceIndex()
        {
            var model = balService.GetListTypeCar();
            ViewBag.Listcar = balCar.GetListCar();
            return View(model);
        }
        public ActionResult GetListTypeCar()
        {
            var model = balService.GetListTypeCar();
            return PartialView(model);
        }
        public bool InsertService(string data)
        {
            try
            {
                bdService vm = JsonConvert.DeserializeObject<bdService>(data);
                balService.InsertService(vm);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public JsonResult GetServiceByID(int IdService)
        {
            var model = balService.GetServiceByID(IdService);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public bool DeleteService(int IdService)
        {
            return balService.DeleteService(IdService);
        }

        #region dịch vụ bảo dưỡng
        public ActionResult ServiceBaoduongIndex()
        {
            var model = balService.GetListTypeCar();
            ViewBag.Listcar = balCar.GetListCar();
            return View(model);
        }
        #endregion
    }
}