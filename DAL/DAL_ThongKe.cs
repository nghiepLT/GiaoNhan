using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.ViewModels;
namespace DAL
{
    public class DAL_ThongKe
    {
        KpiManagerEntities dbcontext = new KpiManagerEntities();
        public IEnumerable<tbReceived> BaoCaoNhapKhoTheoNgay(int UserID,DateTime fromdate,DateTime todate)
        {
            var model = dbcontext.tbReceiveds.ToList().Where(m => m.UserID == UserID && (m.DateStart.Date >= fromdate.Date && m.DateStart.Date <= todate.Date)).ToList();
            return model;
        }
        public IEnumerable<tbTracking> BaoCaoXuatKhoTheoNgay(int UserID, DateTime fromdate, DateTime todate)
        {
            var model = dbcontext.tbTrackings.ToList().Where(m => m.UserID == UserID && (m.DateStart.Value.Date >= fromdate.Date && m.DateStart.Value.Date <= todate.Date)).ToList();
            return model;
        }

        public IEnumerable<VM_Received> ThongKeNhapChiTiet(DateTime date,int UserID)
        {
            var model = dbcontext.tbReceiveds.ToList().Where(m => m.DateStart.Date == date.Date && m.UserID == UserID);
            var result = (from n in model
                          select new VM_Received()
                          {
                              DateEnd=n.DateEnd,
                              DateStart=n.DateStart,
                              KPI=n.Kpi.Value *(-1),
                              Products=n.Products,
                              Type=n.Type,
                              ToTals=n.SLNhap>0?n.SLNhap:n.SlKiemTra,
                              ToTalTimes= n.DateEnd != null ? int.Parse(Math.Round(double.Parse((n.DateEnd.Value.TimeOfDay.TotalSeconds - n.DateStart.TimeOfDay.TotalSeconds).ToString())).ToString()) : 0,
                              TimesResult = n.DateEnd != null ?Tool.Helper.ReturnTime(int.Parse(Math.Round(double.Parse((n.DateEnd.Value.TimeOfDay.TotalSeconds - n.DateStart.TimeOfDay.TotalSeconds).ToString())).ToString())):""
                          });
            return result;
        }
        public IEnumerable<VM_Tracking> ThongKeXuatChiTiet(DateTime date, int UserID)
        {
            var model = dbcontext.tbTrackings.ToList().Where(m => m.DateStart.Value.Date == date.Date && m.UserID == UserID);
            var result = (from n in model
                          select new VM_Tracking()
                          {
                              DateEnd = n.DateEnd,
                              DateStart = n.DateStart.Value,
                              KPI = n.KPI.Value * (-1), 
                              ToTals = n.CountStep,
                              TrackingCode=n.TrackingCode,
                              ToTalTimes = n.DateEnd != null ? int.Parse(Math.Round(double.Parse((n.DateEnd.Value.TimeOfDay.TotalSeconds - n.DateStart.Value.TimeOfDay.TotalSeconds).ToString())).ToString()) : 0,
                              TimesResult = n.DateEnd != null ? Tool.Helper.ReturnTime(int.Parse(Math.Round(double.Parse((n.DateEnd.Value.TimeOfDay.TotalSeconds - n.DateStart.Value.TimeOfDay.TotalSeconds).ToString())).ToString())) : ""
                          });
            return result;
        }
    }
}
