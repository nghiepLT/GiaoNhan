using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using DAL.ViewModels;
namespace DAL
{
    public class DAL_Tracking
    {
        KpiManagerEntities dbContext = new KpiManagerEntities();
        public bool IsExist(tbTracking tbTracking)
        {
            return dbContext.tbTrackings.Any(m => m.TrackingCode == tbTracking.TrackingCode && m.TrackingDate == tbTracking.TrackingDate);
        }
        public bool InsertData(tbTracking tbTracking)
        { 

            dbContext.tbTrackings.Add(tbTracking);
            return dbContext.SaveChanges() == 1 ? true : false;
        }
        public bool UpdateData(tbTracking tbTracking)
        {
            try
            {
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
           
        }
        public IEnumerable<VM_Tracking> LoadTracking(int UserID)
        {
            var model=(from m in dbContext.tbTrackings.ToList()
                       where m.UserID == UserID && (m.TrackingDate.Date == DateTime.Now.Date)
                       select new VM_Tracking()
                       {
                           DateEnd=m.DateEnd,
                           DateStart=m.DateStart.Value,
                           KPI= m.KPI!=null?m.KPI.Value *(-1):0, 
                           ToTals=m.Count,
                           CountStep= m.CountStep,
                           TrackingCode=m.TrackingCode,
                           TrackingID=m.TrackingID,
                           ToTalTimes = m.DateEnd!=null? int.Parse(Math.Round(double.Parse((m.DateEnd.Value.TimeOfDay.TotalSeconds - m.DateStart.Value.TimeOfDay.TotalSeconds).ToString())).ToString()):0,
                           TimesResult = m.DateEnd != null ?Tool.Helper.ReturnTime(int.Parse(Math.Round(double.Parse((m.DateEnd.Value.TimeOfDay.TotalSeconds - m.DateStart.Value.TimeOfDay.TotalSeconds).ToString())).ToString())):""
                       }
                       ).OrderByDescending(m => m.DateStart);
            return model;
        }
        public IEnumerable<tbTracking> LoadReportTracking(int UserID,DateTime fromDate,DateTime toDate)
        {
            return dbContext.tbTrackings.Where(m => m.UserID == UserID && (DbFunctions.TruncateTime(m.TrackingDate)>=fromDate && DbFunctions.TruncateTime(m.TrackingDate)<=toDate)).OrderByDescending(m => m.TrackingDate);
        }

        //

        public IEnumerable<VM_TrackingList> LoadTrackingDataList(int UserID, DateTime fromDate, DateTime toDate,string Code)
        {


            var lsitClone = dbContext.tbTrackings.ToList().Where(m => m.DateEnd != null).Where(m => m.UserID == UserID).Where(m => m.DateStart.Value.Date >= fromDate.Date && m.DateStart.Value.Date <= toDate.Date).ToList();
            var model = dbContext.tbTrackings.ToList().Where(m=>m.DateEnd!=null).Where(m => m.UserID == UserID && m.DateStart.Value.Date >= fromDate.Date && m.DateStart.Value.Date <= toDate.Date);
            if (Code != null && Code.Contains("GN"))
            {
                model = model.Where(m => m.TrackingCode == Code);
            }
            var modelgroupby = model.GroupBy(m => m.DateStart.Value.Date);
            
            //
            List<VM_TrackingList> lstVM_TrackingList = new List<VM_TrackingList>();
            foreach (var item in modelgroupby)
            {
                int total = 0;
                VM_TrackingList _VM_TrackingList = new VM_TrackingList();
                _VM_TrackingList.DateCreate = item.Key.Date;
                var lstChild = (from s in lsitClone
                                where s.DateStart.Value.Date == item.Key.Date
                                select new VM_Tracking()
                                {
                                    DateEnd = s.DateEnd,
                                    DateStart = s.DateStart.Value,
                                    KPI = s.KPI.Value*(-1),
                                    ToTals = s.Count,
                                    CountStep=s.CountStep,
                                    TrackingID = s.TrackingID,
                                    TrackingCode = s.TrackingCode,
                                    ToTalTimes = int.Parse(Math.Round(double.Parse((s.DateEnd.Value.TimeOfDay.TotalSeconds - s.DateStart.Value.TimeOfDay.TotalSeconds).ToString())).ToString())
                                }
                           ).ToList();
                if (Code != null && Code.Contains("GN"))
                {
                    lstChild = lstChild.Where(m => m.TrackingCode == Code).ToList();
                }
                _VM_TrackingList.lstVMTracking = lstChild;
                _VM_TrackingList.Totals = lstChild.Sum(m => m.ToTals);
                _VM_TrackingList.CountStep = lstChild.Sum(m => m.CountStep);
                _VM_TrackingList.TotalRow = lstChild.Count();
                _VM_TrackingList.KPI = lstChild.Sum(m => m.KPI) ;
                _VM_TrackingList.ToTalTimes = lstChild.Sum(m => m.ToTalTimes);
                _VM_TrackingList.TimesResult = Tool.Helper.ReturnTime(_VM_TrackingList.ToTalTimes);
                lstVM_TrackingList.Add(_VM_TrackingList);
            }
            return lstVM_TrackingList;
        }
        public IEnumerable<VM_ReceivedList> LoadRecivedDataList(int UserID, DateTime fromDate, DateTime toDate)
        {
            var modelRc = (from rc in dbContext.tbReceiveds
                         where rc.UserID == UserID
                         select new VM_Received()
                         {
                             DateEnd = rc.DateEnd,
                             DateStart = rc.DateStart,
                             ReceivedID = rc.ReceivedID,
                             STT = rc.STT,
                             KPI= rc.Kpi!=null?rc.Kpi.Value*(-1):0,
                             Type = rc.Type,
                             SLNhap=rc.SLNhap.Value,
                             SlKiemTra=rc.SlKiemTra.Value,
                             Products=rc.Products, 
                             ToTals=rc.SLNhap>0?rc.SLNhap:rc.SlKiemTra
                         }
                       );

            List<VM_Received> lstCclone = new List<VM_Received>();
            foreach (var clone in modelRc.Where(m=>m.DateEnd!=null).ToList())
            {
                lstCclone.Add(clone);
            }
            lstCclone = lstCclone.ToList().Where(m => m.DateStart.Date >= fromDate.Date && m.DateStart.Date <= toDate.Date).ToList();
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
                    item.ToTals = item.SLNhap>0?item.SLNhap : item.SlKiemTra;
                    item.KPIResults = CheckKPI(item.DateStart, item.DateEnd, item.ToTals, item.Type);
                    var totalsecond = item.DateEnd.Value.TimeOfDay.TotalSeconds - item.DateStart.TimeOfDay.TotalSeconds;
                    item.ToTalTimes = int.Parse(Math.Round(double.Parse(totalsecond.ToString())).ToString());
                    item.TimesResult = Tool.Helper.ReturnTime(item.ToTalTimes);

                }

            } 

            var model = dbContext.tbReceiveds.ToList().Where(m => m.UserID == UserID && m.DateStart.Date >= fromDate.Date && m.DateStart.Date <= toDate.Date);
            var modelgroupby = model.GroupBy(m => m.DateStart.Date);


            List<VM_ReceivedList> lstVM_ReceivedList = new List<VM_ReceivedList>();
            foreach (var item in modelgroupby)
            {
                int total = 0;
                VM_ReceivedList _VM_ReceivedList = new VM_ReceivedList();
                _VM_ReceivedList.DateCreate = item.Key.Date;
                var lstchild = lstCclone.ToList().Where(m => m.DateStart.Date == item.Key.Date).ToList();
                _VM_ReceivedList.lstVMReceived = lstchild;
                _VM_ReceivedList.Totals = lstchild.Sum(m => m.ToTals);
                _VM_ReceivedList.KPI = lstchild.Sum(m => m.KPI);
                _VM_ReceivedList.TotalRow = lstchild.Count();
                _VM_ReceivedList.ToTalTimes = lstchild.Sum(m => m.ToTalTimes);
                _VM_ReceivedList.TimesResult = Tool.Helper.ReturnTime(_VM_ReceivedList.ToTalTimes);
                lstVM_ReceivedList.Add(_VM_ReceivedList);
            }
            return lstVM_ReceivedList;
        }
        public IEnumerable<VM_Received> LoadReceivedData(int UserID, DateTime fromDate, DateTime toDate)
        {

            var model = (from rc in dbContext.tbReceiveds
                         where rc.UserID == UserID
                         select new VM_Received()
                         {
                             DateEnd = rc.DateEnd,
                             DateStart = rc.DateStart,
                             ReceivedID = rc.ReceivedID,
                             STT = rc.STT,
                             Type = rc.Type
                         }
                       );

            List<VM_Received> lstCclone = new List<VM_Received>();
            foreach (var clone in model.ToList())
            {
                lstCclone.Add(clone);
            }
            lstCclone = lstCclone.ToList().Where(m => m.DateStart.Date >= fromDate.Date && m.DateStart.Date <= toDate.Date).ToList();
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
                    item.ToTals = totals;
                    item.KPIResults = CheckKPI(item.DateStart, item.DateEnd, item.ToTals, item.Type);
                }

            }
            return lstCclone.OrderByDescending(m => m.DateStart);
        }
        public IEnumerable<tbDriver> GetAllDriver()
        {
            return dbContext.tbDrivers;
        }
        public string CheckKPI(DateTime dateStart,DateTime ?dateEnd,int ?Totals,int ? Type)
        {
            DAL_Config dalConfig = new DAL_Config();
            var config = dalConfig.Getconfig();
            if (dateEnd != null)
            {
                var minusDateSS = (dateEnd.Value - dateStart).TotalSeconds;
                int? NTG40 = 0;
                int? KTG80 = 0;
                //Nhập
                if (Type == 1)
                {
                    NTG40 = Totals * config.TG40;
                }
                //Kiểm tra
                if (Type == 2)
                {
                    KTG80 = Totals * config.TG80;
                }
                var TGKPI = NTG40 + KTG80;
                var chkKPI = minusDateSS - TGKPI;
                if (chkKPI < 0)
                    return "Đạt";
                else
                    return "THIEU KPI CV";
            }
            return "THIEU KPI CV";
        }

        public bool CheckExistTracking(string TrackingCode, DateTime TrackingDate)
        {
            var model = dbContext.tbTrackings.FirstOrDefault(m => m.TrackingCode == TrackingCode && m.TrackingDate == TrackingDate);
            return model!=null? true :false;
        }

        public tbTracking CheckExist(string TrackingCode,int accountID)
        {
            return dbContext.tbTrackings.OrderByDescending(m=>m.TrackingID).Where(m => m.TrackingCode == TrackingCode && m.UserID== accountID).FirstOrDefault();
        }
        public tbTracking CheckExistOnlyCode(string TrackingCode)
        {
            return dbContext.tbTrackings.OrderByDescending(m => m.TrackingID).Where(m => m.TrackingCode == TrackingCode).FirstOrDefault();
        }
        public int GetKPI(DateTime ? dateStart, DateTime? dateEnd, int? Totals,string type)
        {
            int kpiResult = 0;
            DAL_Config dalConfig = new DAL_Config();
            var config = dalConfig.Getconfig();
            if (dateEnd != null)
            {
                var minusDateSS = Math.Round((dateEnd.Value - dateStart).Value.TotalSeconds);
                int? NTG40 = 0;
                //Nhập
                if (Totals == null)
                {
                    if(type== "GN")
                    NTG40 =   int.Parse(config.TimeDelivery.ToString());
                    else 
                    NTG40 =   int.Parse(config.TimeAccountant.ToString());

                }
                else
                {
                    NTG40 = Totals * int.Parse(config.TimeTracking.ToString());

                }

                int TGKPI = (NTG40).Value;
                kpiResult = int.Parse(minusDateSS.ToString()) - TGKPI; 
            }
            return kpiResult;
        }

        public bool CheckCodeDelivery(string Code)
        {
            tbTracking tbtt = dbContext.tbTrackings.Where(m => m.TrackingCode == Code).FirstOrDefault();
            if (tbtt != null)
                return true;
            return false;
        }

        //Kiểm tra quét lần 2
        public bool CheckedCode(int UserID,string TrackingCode)
        {
            return dbContext.tbTrackings.Any(m => m.UserID == UserID && m.DateEnd!=null  && m.TrackingCode == TrackingCode);
        }
        //Kiểm tra số lượng phiếu
        public int CheckedNumber( string TrackingCode)
        {
            var chk = dbContext.tbTrackings.Where(m => m.TrackingCode == TrackingCode).FirstOrDefault();
            if (chk == null)
                return -1;
            var total = chk.Count-chk.CountStep ;
            return total;
        }
        public Dictionary<int,int> CheckSoLuong(int TrackingID,string TrackingCode)
        {
            var current = dbContext.tbTrackings.Find(TrackingID);
            var dif = dbContext.tbTrackings.Where(m => m.TrackingCode == TrackingCode && m.TrackingID!=TrackingID);
            var difcount = dif.Count();
            int type = 0;
            if (difcount == 0)
                type = 1;
            else
                type = 2;
            int countSteptotal = difcount!=0? dif.Sum(m => m.CountStep):current.CountStep;
            var result = current.Count - countSteptotal;
            Dictionary<int, int> test = new Dictionary<int, int>();
            test.Add(result, type);
            return test;
        }
        public bool UpdateCountStep(int TrackingID, int CountStep)
        {
            tbTracking tk = dbContext.tbTrackings.Find(TrackingID);
            try
            {
                if (tk != null)
                {
                    tk.CountStep = CountStep;
                    dbContext.SaveChanges(); 
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public int GetSLRemain(string TrackingCode)
        {
            var current = dbContext.tbTrackings.Where(m=>m.TrackingCode==TrackingCode).FirstOrDefault().Count- dbContext.tbTrackings.Where(m => m.TrackingCode == TrackingCode).Sum(m => m.CountStep);
            return current;
        }
    }
}
