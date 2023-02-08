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
    public class CarController : Controller
    {
        BAL_Car balCar = new BAL_Car();
        #region Typecar
        public ActionResult TypeCarIndex()
        {
            var model = balCar.GetListTypeCar();
            return View(model);
        }
        public ActionResult GetListTypeCar()
        {
            var model = balCar.GetListTypeCar();
            return PartialView(model);
        }
        public bool InsertTypeCar(string data)
        {
            try
            {
                bdTypeCar vm = JsonConvert.DeserializeObject<bdTypeCar>(data);
                balCar.InsertTypeCar(vm);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public JsonResult GetTypeCarByID(int TypeCar)
        {
            var model = balCar.GetTypeCarByID(TypeCar);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public bool DeleteTypeCar(int TypeCar)
        {
            return balCar.DeleteTypeCar(TypeCar);
        }
        #endregion
        #region Car
        // GET: Car
        public ActionResult CarIndex()
        {
            ViewBag.ListTypeCar = balCar.GetListTypeCar();
            ViewBag.ListTaiXe = balCar.GetListTaiXe();
            var model = balCar.GetListTypeCar();
            return View(model);
        }
        public ActionResult CarChildIndex(int TypeCar)
        {
            var model = balCar.GetListCarByID(TypeCar);
            return PartialView(model);
        }
        public bool InsertCar(string data)
        {
            try
            {
                bdCar vm = JsonConvert.DeserializeObject<bdCar>(data);
                balCar.InsertCar(vm);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public JsonResult GetCarByID(int IDCar)
        {
            var model = balCar.GetCarByID(IDCar);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Tai xế
        public ActionResult TaixeIndex()
        { 
            var model = balCar.GetListTaiXe();
            return View(model);
        }
        public bool InsertTaiXe(string data)
        {
            try
            {
                bdTaixe vm = JsonConvert.DeserializeObject<bdTaixe>(data);
                balCar.InsertTaiXe(vm);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public JsonResult GetTypeTaiXerByID(int IdTaixe)
        {
            var model = balCar.GetTaiXeByID(IdTaixe);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public bool DeleteTaiXe(int IdTaixe)
        {
            return balCar.DeleteTaiXe(IdTaixe);
        }
        #endregion

        #region Hành trình xe
        public ActionResult HanhTrinhXe()
        {
            ViewBag.Listcar = balCar.GetListCar();
            return View();
        }

        public bool InsertDotBaoDuong(int IDCar,DateTime NgayBD,int SokmDau)
        {
            try
            {
                balCar.InsertDotBaoDuong(IDCar, NgayBD, SokmDau);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public ActionResult GetDotBaoDuong(int IDCar)
        {
            var model = balCar.GetDotBaoDuong(IDCar);
            ViewBag.Listchild= balCar.GetHanhtrinhbaotri(IDCar);
            return PartialView(model);
        }
        public ActionResult GetHanhTrinh(int IdDotBaoDuong)
        {
            var model = balCar.GetHanhtrinhbaotri(IdDotBaoDuong);
            return PartialView(model);
        }
        public bool CapnhatsoKM(int IDCar, int SoKM)
        {
            return balCar.CapnhatsoKM(IDCar, SoKM);
        }
        public bool DeleteHanhtrinh(int IDHanhTrinhBaoTri)
        {
            return balCar.DeleteHanhtrinh(IDHanhTrinhBaoTri);
        }
        #endregion
    }
}