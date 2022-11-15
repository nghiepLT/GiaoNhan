using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Models;
using DAL;
using BAL;
using Newtonsoft.Json;
using System.Net;

namespace GiaoNhan.Controllers
{
    public class ReceivedController : Controller
    {
        // GET: Received
        BAL_User account = new BAL_User();
        BAL_Received received = new BAL_Received();
        BAL_Permission permission = new BAL_Permission();
        public ActionResult Index()
        {
            //var url = "http://banhangnk-2021.nguyenkimvn.com.vn/jstotalpx.asp?SPX=X221012092-L";

            //var textFromFile = (new WebClient()).DownloadString(url);
            if (Request.Cookies["trakinglogin"] != null)
            {
                ViewBag.userName = Request.Cookies["trakinglogin"].Value;
                tbUSer user = account.GetAccountID(ViewBag.userName);
                ViewBag.UserId = user.UserID;
                ViewBag.Type = 1;
                ViewBag.Permission = permission.GetByID(user.PermissionID);
            } 
            return View();
        }
        //Trung chuyển
        public ActionResult TrungChuyen()
        {
            if (Request.Cookies["trakinglogin"] != null)
            {
                ViewBag.userName = Request.Cookies["trakinglogin"].Value;
                tbUSer user = account.GetAccountID(ViewBag.userName);
                ViewBag.UserId = user.UserID;
                ViewBag.Type = 2;
            }
            return View();
        }
        public ActionResult XepHang()
        {
            if (Request.Cookies["trakinglogin"] != null)
            {
                ViewBag.userName = Request.Cookies["trakinglogin"].Value;
                tbUSer user = account.GetAccountID(ViewBag.userName);
                ViewBag.UserId = user.UserID;
                ViewBag.Type = 3;
            }
            return View();
        }

        [HttpPost]
        public bool Insert(string data)
        {
            if (Request.Cookies["trakinglogin"] != null)
            {
                ViewBag.userName = Request.Cookies["trakinglogin"].Value;
                tbUSer user = account.GetAccountID(ViewBag.userName); 
                tbReceived tbReceived = JsonConvert.DeserializeObject<tbReceived>(data);
                tbReceived.DateStart = DateTime.Now;
                return received.Insert(tbReceived, user.UserID);
            }
            return false;
        }
        public ActionResult Edit(int ReceivedID,int? type)
        {
            if (Request.Cookies["trakinglogin"] != null)
            {
                ViewBag.userName = Request.Cookies["trakinglogin"].Value;
                tbUSer user = account.GetAccountID(ViewBag.userName);
                ViewBag.UserId = user.UserID;
                ViewBag.Permission = permission.GetByID(user.PermissionID);
            }
            var model = received.GetById(ReceivedID);
            ViewBag.ListNCC = received.GetNCC();
            ViewBag.ListProduct = received.GetProduct();
            ViewBag.ReceivedID = ReceivedID;
            ViewBag.Type = type;
            return View(model);
        }
        [HttpPost]
        public bool  Update(int ReceivedID,int NCCID,string Products,int SLNhap,int  SlKiemTra,int type)
        { 
            return received.Update(ReceivedID,NCCID, Products, SLNhap, SlKiemTra, type);
        }
        public ActionResult LoadData(int UserID,int Type)
        {
            var model = received.GetAllByIDUser(UserID).OrderByDescending(m=>m.DateStart).ToList();
            ViewBag.Type = Type;
            return PartialView(model);
        }
        [HttpPost]
        public bool DeleteRecived(int ReceivedID)
        {
            return received.DeleteRecived(ReceivedID);
        }

        public JsonResult GetPopupInfo(int ReceivedID)
        {
            var model = received.GetPopupInfo(ReceivedID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}