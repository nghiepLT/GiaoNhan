using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AForge.Video.DirectShow;
using Newtonsoft.Json;
using DAL.Models;
using BAL;
using DAL.ViewModels;
using System.Net;
using DAL;

namespace GiaoNhan.Controllers
{
    public class TrackingController : Controller
    {
        // GET: Tracking
        BAL_Tracking tracking = new BAL_Tracking();
        BAL_User user = new BAL_User();
        BAL_Permission Permission = new BAL_Permission();
        BAL_Config balConfig = new BAL_Config();
        public ActionResult Index()
        {
            if (Request.Cookies["trakinglogin"] != null)
            {
                var userName = Request.Cookies["trakinglogin"].Value;
                if (userName.ToLower() == "TheTai".ToLower())
                    ViewBag.Type = 1;
                else
                    ViewBag.Type = 2;
            } 
            return View();
        }

        public ActionResult LoadTracking()
        {
            var userName = Request.Cookies["trakinglogin"].Value;
            
            int accountID = user.GetAccountID(userName).UserID;
            var model = tracking.LoadTracking(accountID);
            return PartialView(model);
        } 

        public VM_Json GetJsonData(string spx)
        {
            try
            {
                string firstUrl = balConfig.Getconfig().ApiUrl;
                var url = firstUrl+"jstotalpx.asp?SPX=" + spx;
                var textFromFile = (new WebClient()).DownloadString(url);
                VM_Json vm = JsonConvert.DeserializeObject<VM_Json>(textFromFile);
                return vm; 
            }
            catch(Exception ex)
            {
                return null;
            }
           
        }
        public int GetKPIKT(DateTime? dateStart, DateTime? dateEnd, int? Totals)
        {
            int kpiResult = 0;
            DAL_Config dalConfig = new DAL_Config();
            var config = dalConfig.Getconfig();
            if (dateEnd != null)
            {
                if (dateEnd.Value != DateTime.MinValue)
                {
                    var minusDateSS = Math.Round((dateEnd.Value - dateStart).Value.TotalSeconds);
                    int? NTG40 = 0;
                    //Nhập
                    NTG40 = int.Parse(config.TimeAccountant.ToString());

                    int TGKPI = (NTG40).Value;
                    kpiResult = int.Parse(minusDateSS.ToString()) - TGKPI;
                }
              
            }
            return kpiResult;
        }

        //0 thất bại
        //1 là hợp lệ
        //2 là mã ko hợp lệ 
        //3 là mã đã quét
        //4 số lượng đã đủ
        public int Insert(string data)
        {
            if (Request.Cookies["trakinglogin"] != null)
            {
                var userName = Request.Cookies["trakinglogin"].Value;
                int accountID = user.GetAccountID(userName).UserID;
                tbTracking tbAccount = JsonConvert.DeserializeObject<tbTracking>(data);

               
                //Kiểm tra phiếu có trong hệ thống không
                var getJson = GetJsonData(tbAccount.TrackingCode);
                if (getJson == null)
                    return 2;
                //Kiểm tra mã đã quét chưa
                //if (tracking.CheckedCode(accountID, tbAccount.TrackingCode))
                //{
                //    return 3;
                //}
                //Kiểm tra nếu đã đủ số lượng thì không cho quyet1 tiếp
                int SoLuongconlai = 0;
                SoLuongconlai = tracking.CheckedNumber(tbAccount.TrackingCode);
                string type = "";
                var getPermission = user.GetbyID(accountID);
                if (getPermission != null)
                {
                    int getPermissionID = getPermission.PermissionID;
                    var getNamePermission = Permission.GetByID(getPermissionID).PermissionName;

                    if (getNamePermission == "Xuất kho")
                    {
                        if (tbAccount.TrackingCode.Contains("GN") || tbAccount.TrackingCode.Contains("KT"))
                        {
                            return 2;
                        }
                    }
                    if (getNamePermission == "Giao nhận")
                    {
                        if (tbAccount.TrackingCode.Contains("X") || tbAccount.TrackingCode.Contains("KT"))
                        {
                            return 2;
                        }
                        type = "GN"; 
                    }
                    if (getNamePermission == "Kế toán")
                    {
                        if (tbAccount.TrackingCode.Contains("X") || tbAccount.TrackingCode.Contains("GN"))
                        {
                            return 2;
                        }
                        type = "KT";
                    }
                }
                
                tbTracking ckhTracking = tracking.CheckExist(tbAccount.TrackingCode, accountID);
                if (ckhTracking==null || (ckhTracking!=null && ckhTracking.DateEnd!=null))
                {
                   
                    if (getJson == null)
                    {
                        if (!tbAccount.TrackingCode.Contains("GN"))
                        {
                            if (!tbAccount.TrackingCode.Contains("KT"))
                            {
                                return 2; 
                            }
                        } 
                    }
                    //Kiểm tra Mã giao nhận
                    //if (tbAccount.TrackingCode.Contains("GN"))
                    //{
                    //    if (!tracking.CheckCodeDelivery(tbAccount.TrackingCode))

                    //    {
                    //        return false;
                    //    }
                    //}
                    tbAccount.DateStart = DateTime.Now;
                    tbAccount.TrackingDate= DateTime.Now;
                    tbAccount.UserID = accountID;
                    if (!tbAccount.TrackingCode.Contains("GN") && !tbAccount.TrackingCode.Contains("KT"))
                    {
                        ViewBag.IsTai = "XK";
                        tbAccount.Count = getJson.ToTal;
                        if(SoLuongconlai==0)
                        {
                            return 4;
                        }
                        // tbAccount.DateCreate = DateTime.ParseExact(getJson.NgayPX, "dd/MM/yyyy hh:mm:ss", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);


                        tbAccount.DateCreate = DateTime.Parse(getJson.NgayPX);
                        //Kiểm tra mã đã quét chưa, nếu quét rồi thì lấy countstep của phiếu cũ
                        var checkbycode = tracking.CheckExistOnlyCode(tbAccount.TrackingCode);
                        if (checkbycode == null)
                        {
                            tbAccount.CountStep = tbAccount.Count;
                        }
                        else
                        {
                            // tbAccount.CountStep = checkbycode.Count - SoLuongconlai;
                            tbAccount.CountStep = SoLuongconlai;
                        }
                        //Cập nhật cho kế toán
                        //if (tbAccount.TrackingID == 0)
                        //{
                        //    tbTracking trackingKetoan = new tbTracking();
                        //    trackingKetoan.DateCreate = DateTime.Parse(getJson.NgayPX);
                        //    trackingKetoan.DateStart = DateTime.Parse(getJson.NgayPX);
                        //    trackingKetoan.DateEnd =!getJson.NgayXuatHD.Contains("00/00/")?DateTime.Parse(getJson.NgayXuatHD): tbAccount.DateStart;
                        //    trackingKetoan.Count = getJson.ToTal;
                        //    trackingKetoan.TrackingCode = getJson.MaPX;
                        //    trackingKetoan.TrackingDate = DateTime.Now;
                        //    trackingKetoan.UserID = user.GetbyCode(getJson.MaNVHD).UserID;
                        //    trackingKetoan.KPI = GetKPIKT(trackingKetoan.DateStart, trackingKetoan.DateEnd, trackingKetoan.Count);
                        //    tracking.InsertData(trackingKetoan);
                        //}

                    }
                        
                    return tracking.InsertData(tbAccount)==true?1:0;
                }
                else
                {
                   

                    ckhTracking.DateEnd = DateTime.Now;
                    if (ckhTracking.DateEnd != null)
                        ckhTracking.CountStep = tbAccount.CountStep;
                    ckhTracking.KPI = tracking.GetKPI(ckhTracking.DateStart, ckhTracking.DateEnd, ckhTracking.CountStep, type);
                    tracking.UpdateData(ckhTracking);
                    return 1;
                }
            }
            return 0;
        }

        [HttpPost]
        public int UpdateCountStep(int TrackingID,int Count, int CountStep,string TrackingCode)
        {
            try
            {
                var getDiction = tracking.CheckSoLuong(TrackingID, TrackingCode);
                var key = getDiction.FirstOrDefault().Key;
                var value= getDiction.FirstOrDefault().Value;
                int total = CountStep - key;

                if (value == 1)
                {
                    if (CountStep > Count)
                    {
                        return key;
                    }
                }
                else
                {
                    if (total > 0 && key != 0)
                    {
                        return key;
                    }
                }
                tracking.UpdateCountStep(TrackingID, CountStep);
                return 0;

            }
            catch (Exception ex)
            {
                return 0;

            }
        } 

        public int GetSLRemain(string TrackingCode)
        {
            return tracking.GetSLRemain(TrackingCode);
        }
    }
}