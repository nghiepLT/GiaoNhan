using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.ViewModels;
namespace DAL
{
    public class Dal_Report
    {
        KpiManagerEntities dbContext = new KpiManagerEntities();

       

        public IEnumerable<VM_Tracking> ReportTrackingDetail(DateTime date, int UserID)
        {
            var model = (from s in dbContext.tbTrackings.ToList()

                         where s.DateStart.Value.Date == date.Date && s.UserID == UserID
                         select new VM_Tracking()
                         {
                             DateEnd = s.DateEnd,
                             DateStart = s.DateStart.Value,
                             KPI = s.KPI != null ? s.KPI.Value * (-1) : 0,
                             ToTals = s.CountStep,
                             TrackingCode = s.TrackingCode,
                             TrackingID = s.TrackingID,
                             ToTalTimes = s.DateEnd != null ? int.Parse(Math.Round(double.Parse((s.DateEnd.Value.TimeOfDay.TotalSeconds - s.DateStart.Value.TimeOfDay.TotalSeconds).ToString())).ToString()) : 0,
                             TimesResult = s.DateEnd != null ? Tool.Helper.ReturnTime(int.Parse(Math.Round(double.Parse((s.DateEnd.Value.TimeOfDay.TotalSeconds - s.DateStart.Value.TimeOfDay.TotalSeconds).ToString())).ToString())) : ""
                         }
                         );

            return model;
        }
        public IEnumerable<VM_Received> ReportReceivedDetail(DateTime date, int UserID)
        {
            var model = (from s in dbContext.tbReceiveds.ToList()

                         where s.DateStart.Date == date.Date && s.UserID == UserID
                         select new VM_Received()
                         {
                             DateEnd = s.DateEnd,
                             DateStart = s.DateStart,
                             KPI = s.Kpi.Value * (-1),
                             Type = s.Type,
                             ReceivedID = s.ReceivedID,
                             SlKiemTra = s.SlKiemTra.HasValue?s.SlKiemTra.Value:0,
                             SLNhap = s.SLNhap.HasValue?s.SLNhap.Value:0,
                             Products=s.Products
                         }
                         );
            List<VM_Received> lstCclone = new List<VM_Received>();
            foreach (var clone in model.ToList())
            {
                lstCclone.Add(clone);
            }
            foreach (var item in lstCclone)
            {
                string str = "";
                int? totals = 0;
                var detail = dbContext.tbReceivedDetails.Where(m => m.ReceivedID == item.ReceivedID);
                if (detail != null)
                {
                    foreach (var item2 in detail)
                    {
                        tbProduct prod = dbContext.tbProducts.Find(item2.ProductID);
                        str += item2.Count + " " + prod.ProductName + ", ";
                        totals += item2.Count;
                    }
                    item.ProductDescription = str;
                    if(item.SLNhap>0 && item.SlKiemTra>0)
                    {
                        item.ToTals = item.SLNhap ;
                    }
                    else
                    {
                        if(item.SLNhap > 0)
                        {
                            item.ToTals = item.SLNhap;
                        }
                        if (item.SlKiemTra > 0)
                        {   
                            item.ToTals = item.SlKiemTra;
                        }
                    }
                    var totalsecond = item.DateEnd.Value.TimeOfDay.TotalSeconds - item.DateStart.TimeOfDay.TotalSeconds;
                    item.ToTalTimes = int.Parse(Math.Round(double.Parse(totalsecond.ToString())).ToString());
                    item.TimesResult = Tool.Helper.ReturnTime(item.ToTalTimes);
                    item.SlKiemTra = item.SlKiemTra;
                }
            }
            return lstCclone.OrderByDescending(m => m.STT);
        }
        public IEnumerable<VM_TheTaiReport> ReportGiaoNhan(DateTime fromdate, DateTime toddate,string MaThe)
        {
            var model = (from tt in dbContext.tbTheTais.ToList()
                         where tt.DateStart.Value.Date >= fromdate.Date && tt.DateStart.Value.Date <= toddate.Date
                         && (MaThe == "0" || (MaThe != "0" && MaThe == tt.MaThe) )
                         select new VM_TheTaiReport()
                         {
                             Count = tt.Count!=null? tt.Count:0,
                             DateStart = tt.DateStart.Value,
                             DateEnd = tt.DateEnd!=null? tt.DateEnd.Value:DateTime.MinValue,
                             MaPhieu = tt.MaPhieu,
                             MaThe = tt.MaThe,
                             ThetaiID = tt.ThetaiID,
                             UserId = tt.UserId, 
                             lstTheTaiChiTiet = dbContext.tbTheTaiChiTiets.Where(m => m.ThetaiID == tt.ThetaiID).OrderBy(m=>m.DateEnd.Value).ToList(),
                             Luotve= tt.Luotve.HasValue? tt.Luotve.Value:DateTime.MinValue,
                             ToTalTimes = tt.DateEnd!=null? int.Parse(Math.Round(double.Parse((tt.DateEnd.Value.TimeOfDay.TotalSeconds - tt.DateStart.Value.TimeOfDay.TotalSeconds).ToString())).ToString()):0,
                             TongThoigian = (tt.DateEnd!=null && dbContext.tbTheTaiChiTiets.OrderBy(m=>m.DateEnd.Value).ToList().Where(m => m.ThetaiID == tt.ThetaiID && m.DateEnd != null).LastOrDefault()!=null) ? Tool.Helper.ReturnTime(int.Parse(Math.Round(double.Parse((tt.DateStart.Value.TimeOfDay.TotalSeconds - dbContext.tbTheTaiChiTiets.OrderBy(m => m.DateEnd.Value).ToList().Where(m => m.ThetaiID == tt.ThetaiID && m.DateEnd!=null).LastOrDefault().DateEnd.Value.TimeOfDay.TotalSeconds).ToString())).ToString())*(-1)):"",
                             TienPhatSinh= int.Parse(dbContext.tbTheTaiChiTiets.Where(m => m.ThetaiID == tt.ThetaiID).Sum(m => m.TienPhatSinh).ToString())
                         }
                       );

            return model;
        }
    }
}