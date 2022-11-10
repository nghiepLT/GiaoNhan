using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using DAL.Models;
using DAL.ViewModels;
using System.Globalization;
using System.Net;
using System.Net.Http;

namespace GiaoNhan.Controllers
{
    public class ReportController : Controller
    {
        BAL_Tracking tracking = new BAL_Tracking();
        BAL_User account = new BAL_User();
        BAL_Permission Permission = new BAL_Permission();
        BAL_Report balReport = new BAL_Report();
        // GET: Report
        public ActionResult ReportByDay()
        { 
            var url = "http://banhangnk-2021.nguyenkimvn.com.vn/jstotalpx.asp?SPX=X221012092-L";
            using (HttpClient client = new HttpClient())
            {
                string result = client.GetStringAsync(url).Result;

            } 
            ViewBag.Permission = Permission.GetALl();
            var model = account.GetAll();
            ViewBag.ListDelivery = account.GetAll().Where(m => m.UserName == "");

            if (Request.Cookies["trakinglogin"] != null)
            {
                ViewBag.userName = Request.Cookies["trakinglogin"].Value;
                tbUSer user = account.GetAccountID(ViewBag.userName);
                ViewBag.PermissionID = user.PermissionID;
                ViewBag.UserID = user.UserID;

            }
            return View(model);
        } 

        [HttpPost]
        public ActionResult LoadUserByPermission(int PermissionID)
        {
            var model = account.GetByPermissionId(PermissionID).Where(m=>m.UserName!="");
            return PartialView(model);
        }
        public ActionResult ReportAveraged()
        {
            var model = account.GetAll();
            ViewBag.ListDriver = tracking.GetAllDriver();
            return View(model);
        }
        public ActionResult LoadTrackingData(DateTime fromDate,DateTime toDate,int UserID)
        {
            var model = tracking.LoadReportTracking(UserID, fromDate, toDate);
            return PartialView(model);
        }
        public ActionResult LoadTrackingData2(string fromDate, string toDate, int UserID,string Code)
        {
            var getPermissionID = account.GetbyID(UserID).PermissionID;
            var getNamePermission = Permission.GetByID(getPermissionID).PermissionName;
            if(getNamePermission=="Giao nhận")
            {
                ViewBag.Type = "GN";
            }
            if (getNamePermission == "Kế toán")
            {
                ViewBag.Type = "KT";
            }
            DateTime _fromDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime _toDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.FromDate = _fromDate.ToShortDateString();
            ViewBag.ToDate = _toDate.ToShortDateString();
            var model = tracking.LoadTrackingDataList(UserID, _fromDate, _toDate,Code);
            VM_TrackingListTotal total = new VM_TrackingListTotal();
            total.Totals = model.Sum(m => m.Totals);
            total.CountStep = model.Sum(m => m.CountStep);
            total.TotalRow = model.Sum(m => m.TotalRow);
            total.KPI = model.Sum(m => m.KPI);
            total.ToTalTimes = model.Sum(m => m.ToTalTimes);
            total.TimesResult = Tool.Helper.ReturnTime(total.ToTalTimes);
            ViewBag.Total = total;
            return PartialView(model);
        } 

        public ActionResult ReportReceived()
        {
            ViewBag.ListUser = account.GetByPermissionId(2);
            return View();
        }
        public ActionResult LoadReceivedData(string fromDate, string toDate, int UserID)
        {

            DateTime _fromDate= DateTime.ParseExact(fromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime _toDate = DateTime.ParseExact(toDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
             
            var model = tracking.LoadReceivedData(UserID, _fromDate, _toDate);
            return PartialView(model);  
        }
        public ActionResult LoadReceivedDataList(string fromDate, string toDate, int UserID)
        { 
            DateTime _fromDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime _toDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.FromDate = _fromDate.ToShortDateString();
            ViewBag.ToDate = _toDate.ToShortDateString();
            var model = tracking.LoadReceivedDataList(UserID, _fromDate, _toDate).OrderByDescending(m=>m.DateCreate);
            VM_ReceivedListTotal total = new VM_ReceivedListTotal();
            total.Totals = model.Sum(m => m.Totals);
            total.TotalRow = model.Sum(m => m.TotalRow);
            total.KPI = model.Sum(m => m.KPI);
            total.ToTalTimes = model.Sum(m => m.ToTalTimes);
            total.TimesResult = Tool.Helper.ReturnTime(total.ToTalTimes);
            ViewBag.Total = total; 
            return PartialView(model);
        }
        //Detail
        public ActionResult ReportTrackingDetail(string date,int UserID,string Code)
        {
            var getPermissionID = account.GetbyID(UserID).PermissionID;
            var getNamePermission = Permission.GetByID(getPermissionID).PermissionName;
            if (getNamePermission == "Giao nhận")
            {
                ViewBag.Type = "GN";
            }
            if (getNamePermission == "Kế toán")
            {
                ViewBag.Type = "KT";
            }

            DateTime getDate= DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            var model = balReport.ReportTrackingDetail(getDate, UserID);
            if(Code!=null && Code.Contains("GN"))
            {
                model = model.Where(m => m.TrackingCode == Code);
            }
            ViewBag.StartDate = getDate.Date;
            return PartialView(model);
        }
        public ActionResult ReportReceivedDetail(string date,int UserID)
        {
            DateTime getDate = DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            var model = balReport.ReportReceivedDetail(getDate, UserID);
            ViewBag.StartDate = getDate.Date;
            return PartialView(model);
        }

        public ActionResult ThongKeThongKeGiaoNhan()
        {
            var model = account.GetAll().Where(m => m.Code != null && m.Code.Contains("GN") && m.UserID != 1019);

            return View(model);
        }
        public ActionResult ThongKeThongKeGiaoNhanData(string fromDate, string toDate,string MaThe)
        {
            DateTime _fromDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime _toDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var model = balReport.ReportGiaoNhan(_fromDate, _toDate, MaThe);
            return PartialView(model);
        }
    }
}