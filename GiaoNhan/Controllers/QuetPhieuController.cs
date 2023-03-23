using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using DAL.Models;
using BAL;
using DAL.ViewModels;
using System.Net;
using DAL;
using System.Globalization;

namespace GiaoNhan.Controllers
{
    public class QuetPhieuController : Controller
    {
        BAL_Tracking tracking = new BAL_Tracking();
        BAL_User user = new BAL_User();
        BAL_Permission Permission = new BAL_Permission();
        BAL_Config balConfig = new BAL_Config();
        BAL_Received received = new BAL_Received();
        BAL_QuetThe balQuetThe = new BAL_QuetThe();
        // GET: QuetPhieu
        public ActionResult Index()
        {
            return View();
        }
        public VM_Json GetJsonData(string spx)
        {
            try
            {
                string firstUrl = balConfig.Getconfig().ApiUrl;
                var url = firstUrl + "jstotalpx.asp?SPX=" + spx;
                var textFromFile = (new WebClient()).DownloadString(url);
                VM_Json vm = JsonConvert.DeserializeObject<VM_Json>(textFromFile);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public int Insert(string TrackingCode)
        {
            if (Request.Cookies["trakinglogin"] != null)
            {
                var userName = Request.Cookies["trakinglogin"].Value;
                int accountID = user.GetAccountID(userName).UserID; 

                //Kiểm tra phiếu có trong hệ thống không
                var getJson = GetJsonData(TrackingCode); 
                if(getJson==null)
                {
                    return -1;
                }
                if (balQuetThe.CheckQuetThe(TrackingCode))
                {
                    return -2;
                }
                return balQuetThe.InsertQuetThe(TrackingCode, accountID);

            }
            return -1;
        }
        public ActionResult LoadTracking()
        {
            var model = balQuetThe.GetListQuetThe();
            return PartialView(model);
        }

        public ActionResult ThongKeQuetPhieu()
        {
            return View();
        }
        public ActionResult ThongKeQuetPhieuData(string fromDate, string toDate,string TrackingCode)
        {
            DateTime _fromDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime _toDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var model = balQuetThe.GetListQuetTheReport(_fromDate, _toDate, TrackingCode);
            return PartialView(model);
        }
    }
}