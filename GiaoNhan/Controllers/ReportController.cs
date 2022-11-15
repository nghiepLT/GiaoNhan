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
        BAL_ThongKe balThongKe = new BAL_ThongKe();
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
            var Model2 = tracking.LoadReceivedDataList(UserID, _fromDate, _toDate).OrderByDescending(m => m.DateCreate);
            total.Totals = model.Sum(m => m.Totals);
            total.Totals += Model2 != null ? Model2.Sum(m => m.Totals) : 0;
            total.CountStep = model.Sum(m => m.CountStep);
            total.CountStep += (Model2 != null ? Model2.Sum(m => m.Totals) : 0).Value;
            total.TotalRow = model.Sum(m => m.TotalRow);
            total.KPI = model.Sum(m => m.KPI);
            total.ToTalTimes = model.Sum(m => m.ToTalTimes);
            total.TimesResult = Tool.Helper.ReturnTime(total.ToTalTimes);
            ViewBag.Total = total;
            //
           
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
            //Truong hop có hập hàng
            ViewBag.Model2 = balReport.ReportReceivedDetail(getDate, UserID);
           
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
            if (Request.Cookies["trakinglogin"] != null)
            {
                ViewBag.userName = Request.Cookies["trakinglogin"].Value;
                tbUSer user = account.GetAccountID(ViewBag.userName); 
                ViewBag.UserID = user.Code;
                var model = account.GetAll().Where(m => m.Code != null && m.Code.Contains("GN") && m.UserID != 1019); 
                return View(model);
            }
            return View();
        }
        public ActionResult ThongKeThongKeGiaoNhanData(string fromDate, string toDate,string MaThe)
        {
            DateTime _fromDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime _toDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var model = balReport.ReportGiaoNhan(_fromDate, _toDate, MaThe);
            return PartialView(model);
        }

        public class ThongKeTong
        {
            public int TongSoDon { get; set; } = 0;
            public int TongSoLuong { get; set; } = 0;
            public string TongThoiGian { get; set; }
            public int TongKPI { get; set; } = 0;
            public string KPIResult { get; set; }
            public int TotalTimes { get; set; } = 0;
        }
        public class ThongKeMoiNgay
        {
            public DateTime NgayTao { get; set; }
            public int SLNhap { get; set; } = 0;
            public int SLXuat { get; set; } = 0;
            public int TongSoLuong { get; set; } = 0;
            public int TotalTimes { get; set; } = 0;
            public string TongThoiGian { get; set; }
            public int TongKPI { get; set; } = 0;
            public string KPIResult { get; set; }
            public int TongSoDon { get; set; } = 0;
            public int Type { get; set; } //1 là Nhập, 2 là xuất
        }

       
        public int ChooseSL(int slnhap,int slkiemtra)
        {
            return slnhap > 0 ? slnhap : slkiemtra;
        }
        public ActionResult ThongKeBaoCao(string fromDate, string toDate, int UserID, string Code)
        {
            DateTime _fromDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime _toDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.FromDate = _fromDate.ToShortDateString();
            ViewBag.ToDate = _toDate.ToShortDateString();
            List<ThongKeMoiNgay> lstThongKeMoiNgay = new List<ThongKeMoiNgay>();
            //Duyệt từ ngày đến ngày
            //Lấy list Nhập từ ngày đến ngày
            var lstNhapByDate = balThongKe.BaoCaoNhapKhoTheoNgay(UserID, _fromDate, _toDate);
            var lstXuatByDate = balThongKe.BaoCaoXuatKhoTheoNgay(UserID, _fromDate, _toDate);
            for (DateTime dt= _fromDate; dt <= _toDate; dt=dt.AddDays(1))
            {
                var rowNhap = lstNhapByDate.ToList().Where(m => m.DateStart.Date == dt.Date);
                var rowXuat= lstXuatByDate.ToList().Where(m => m.DateStart.Value.Date == dt.Date);
                //Add list nhap
                ThongKeMoiNgay ThongKeMoiNgay = new ThongKeMoiNgay();
                if (rowNhap != null && rowNhap.Count()>0)
                {
                    ThongKeMoiNgay.NgayTao = rowNhap.FirstOrDefault().DateStart; 
                    ThongKeMoiNgay.TongSoLuong =rowNhap.Sum(m => (m.SLNhap > 0 && m.SLNhap>0) ? m.SLNhap.Value : (m.SlKiemTra.HasValue?m.SlKiemTra.Value:0));
                    ThongKeMoiNgay.TongKPI = rowNhap.Sum(m => m.Kpi.HasValue?m.Kpi.Value:0);
                    ThongKeMoiNgay.TongKPI = ThongKeMoiNgay.TongKPI * (-1);
                    ThongKeMoiNgay.TotalTimes = (int)Math.Round(rowNhap.Sum(m => m.DateEnd.HasValue? m.DateEnd.Value.TimeOfDay.TotalSeconds - m.DateStart.TimeOfDay.TotalSeconds:0));
                    ThongKeMoiNgay.TongThoiGian = Tool.Helper.ReturnTime(ThongKeMoiNgay.TotalTimes);
                    ThongKeMoiNgay.TongSoDon = rowNhap.Count();
                    ThongKeMoiNgay.SLNhap = rowNhap.Count();
                }
                if (rowXuat != null && rowXuat.Count()>0)
                {
                    ThongKeMoiNgay.NgayTao = rowXuat.FirstOrDefault().DateStart.Value;
                    ThongKeMoiNgay.SLXuat = rowXuat.Count();
                    ThongKeMoiNgay.TongKPI+=rowXuat.Sum(m => m.KPI.HasValue? m.KPI.Value:0);
                    ThongKeMoiNgay.TongKPI = ThongKeMoiNgay.TongKPI * (-1);
                    ThongKeMoiNgay.TotalTimes += (int)Math.Round(rowXuat.Sum(m => m.DateEnd.HasValue? m.DateEnd.Value.TimeOfDay.TotalSeconds - m.DateStart.Value.TimeOfDay.TotalSeconds:0));
                    ThongKeMoiNgay.TongThoiGian = Tool.Helper.ReturnTime(ThongKeMoiNgay.TotalTimes);
                    ThongKeMoiNgay.TongSoDon += rowXuat.Count();
                    ThongKeMoiNgay.TongSoLuong+= rowXuat.Sum(m => m.CountStep);
                }
              
                lstThongKeMoiNgay.Add(ThongKeMoiNgay);
            }
            ViewBag.ListThongKeMoiNgay = lstThongKeMoiNgay;
            //ThongKeTong
            ThongKeTong ThongKeTong = new ThongKeTong();
            ThongKeTong.TongSoDon = lstThongKeMoiNgay.Sum(m => m.TongSoDon);
            ThongKeTong.TongSoLuong = lstThongKeMoiNgay.Sum(m => m.TongSoLuong);
            ThongKeTong.TongKPI = lstThongKeMoiNgay.Sum(m => m.TongKPI);
            ThongKeTong.TotalTimes = lstThongKeMoiNgay.Sum(m => m.TotalTimes);
            ThongKeTong.TongThoiGian= Tool.Helper.ReturnTime(ThongKeTong.TotalTimes);
            ViewBag.ThongKeTong = ThongKeTong;
            return PartialView();
        }

        public ActionResult ThongKeChiTiet(string date, int UserID)
        {
            DateTime _date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.tkNhap = balThongKe.ThongKeNhapChiTiet(_date, UserID);
            ViewBag.tkXuat = balThongKe.ThongKeXuatChiTiet(_date, UserID);
            return PartialView();
        }
    }
}
