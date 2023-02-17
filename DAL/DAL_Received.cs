using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Tool;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using DAL.ViewModels;
namespace DAL
{
    public class DAL_Received
    {
        KpiManagerEntities dbcontext = new KpiManagerEntities();
        public IEnumerable<tbReceived> GetAll()
        {
            return dbcontext.tbReceiveds.Where(m => m.DateStart == DateTime.Now).OrderByDescending(m=>m.STT);
        }
        public VM_Received GetPopupInfo(int ReceivedID)
        {
            var model = (from rc in dbcontext.tbReceiveds.ToList()
                         where rc.ReceivedID == ReceivedID
                         select new VM_Received()
                         {
                             DateEnd = rc.DateEnd,
                             DateStart = rc.DateStart,
                             ReceivedID = rc.ReceivedID,
                             STT = rc.STT,
                             Type = rc.Type, 
                             KPI = rc.Kpi != null ? rc.Kpi.Value * (-1) : 0,
                             ToTalTimes = int.Parse(Math.Round(double.Parse((rc.DateEnd.Value.TimeOfDay.TotalSeconds - rc.DateStart.TimeOfDay.TotalSeconds).ToString())).ToString()),
                             TimesResult = Tool.Helper.ReturnTime(int.Parse(Math.Round(double.Parse((rc.DateEnd.Value.TimeOfDay.TotalSeconds - rc.DateStart.TimeOfDay.TotalSeconds).ToString())).ToString()))
                         }
                       ).FirstOrDefault();

            return model;
        }

        public IEnumerable<VM_Received> GetAllByIDUser(int UserID)
        {
            var model = (from rc in dbcontext.tbReceiveds.ToList()
                         where rc.UserID==UserID
                         select new VM_Received()
                         {
                             DateEnd=rc.DateEnd,
                             DateStart=rc.DateStart,
                             ReceivedID=rc.ReceivedID,
                             STT=rc.STT,
                             Type=rc.Type,
                             Products=rc.Products,
                             SlKiemTra= rc.SlKiemTra!=null?rc.SlKiemTra.Value:0,
                             SLNhap= rc.SLNhap!=null?rc.SLNhap.Value:0,
                             KPI= rc.Kpi!=null?rc.Kpi.Value *(-1):0,
                             ParentId= rc.ParentId.HasValue? rc.ParentId.Value:0,
                             IsSent= rc.IsSent.HasValue?rc.IsSent.Value:0,
                             TypeTracking= rc.TypeTracking.HasValue? rc.TypeTracking.Value:0,
                             Code=rc.Code
                         }
                       ).ToList();

            List<VM_Received> lstCclone = new List<VM_Received>();
            foreach(var clone in model.ToList())
            {
                lstCclone.Add(clone);
            }
            foreach (var item in lstCclone)
            {
                string str = "";
                int ? totals = 0;
                var detail = dbcontext.tbReceivedDetails.Where(m => m.ReceivedID == item.ReceivedID);
                if (detail != null)
                {
                    foreach (var item2 in detail)
                    {
                        tbProduct prod = dbcontext.tbProducts.Find(item2.ProductID);
                        str += item2.Count + " " + prod.ProductName + ", ";
                        totals += item2.Count;
                    }
                    item.ProductDescription = str;
                    item.ToTals = totals;
                }
                 
            } 
            return lstCclone.ToList().Where(m =>m.DateStart.Date == DateTime.Now.Date).OrderByDescending(m => m.STT);
        }
        public tbReceived GetById(int ReceivedID)
        {
            return dbcontext.tbReceiveds.Find(ReceivedID);
        }
        public bool Insert(tbReceived tbReceived, int UserID)
        {
            try
            {
                tbReceived tr = dbcontext.tbReceiveds.ToList().Where(m => m.DateStart.Date == tbReceived.DateStart.Date && m.UserID== UserID).LastOrDefault();
                if (tr == null)
                {
                    tbReceived.STT = 1;
                }
                else
                {
                    tbReceived.STT = tr.STT + 1;
                }
                dbcontext.tbReceiveds.Add(tbReceived);
                dbcontext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
           
        }
        
        public int GetKPI(DateTime dateStart, DateTime? dateEnd, int SLNhap,int SLKiemtra, int? Type)
        {
            int kpiResult=0;
            DAL_Config dalConfig = new DAL_Config();
            var config = dalConfig.Getconfig();
            if (dateEnd != null)
            {
                var minusDateSS =Math.Round((dateEnd.Value - dateStart).TotalSeconds);
                int? NTG40 = 0;
                int? KTG80 = 0;
                //Nhập
                if (Type == 1)
                {
                    NTG40 = SLNhap * config.TG40;
                }
                //Kiểm tra
                if (Type == 1)
                {
                    KTG80 = SLKiemtra * config.TG80;
                }
                if(Type==2 || Type==3)
                {
                    NTG40 = SLNhap * config.TG40;
                }
                int TGKPI = (NTG40 + KTG80).Value;
                kpiResult = int.Parse(minusDateSS.ToString()) - TGKPI; 
            }
            return kpiResult;
        }

        public int GetTotalProducts(  List<tbReceivedDetail> list)
        {
            var totals = list.Sum(m=>m.Count);
            return totals.Value;
        }
        public bool Update(int ReceivedID,int NCCID, string Products, int SLNhap, int SlKiemTra,int type)
        {
            tbReceived tb = dbcontext.tbReceiveds.Find(ReceivedID);
            try
            {
                if (tb != null)
                { 
                    tb.Type = type;
                    tb.NCCID = NCCID;
                    tb.Products = Products;
                    tb.SLNhap = SLNhap;
                    tb.SlKiemTra = SlKiemTra;
                    tb.DateEnd = DateTime.Now; 
                    tb.Kpi = GetKPI(tb.DateStart, tb.DateEnd,SLNhap,SlKiemTra, type);
                    dbcontext.SaveChanges();  
                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            } 
        }
        public IEnumerable<tbNCC> GetNCC()
        {
            return dbcontext.tbNCCs;
        }
        public IEnumerable<tbProduct> GetProduct()
        {
            return dbcontext.tbProducts;
        }
        public bool DeleteRecived(int ReceivedID)
        {
            try
            {
                tbReceived rc = dbcontext.tbReceiveds.Find(ReceivedID);
                dbcontext.tbReceiveds.Remove(rc);
                dbcontext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<tbUSer> GetNhanVienTrungChuyen()
        {
            return dbcontext.tbUSers.Where(m => m.RoleId == 7);
        }
        public IEnumerable<tbUSer> GetNhanVienXepHang()
        {
            return dbcontext.tbUSers.Where(m => m.RoleId == 8);
        }
        public void ChuyenPhieu(int ReceivedID, string UserID,int Time)
        {
            tbReceived tbReceivedCurrent = dbcontext.tbReceiveds.Where(m => m.ReceivedID == ReceivedID).FirstOrDefault();
            tbReceivedCurrent.IsSent = 1;
            dbcontext.SaveChanges();
            var lstSplit = UserID.Split(',');
            foreach(var item in lstSplit)
            {
                if (item != "")
                {
                    tbReceived tbRecived = new tbReceived();
                    tbRecived.ParentId = ReceivedID;
                    tbRecived.UserID = int.Parse(item);
                    tbRecived.NCCID = tbReceivedCurrent.NCCID;
                    tbRecived.DateStart = DateTime.Now;
                    tbRecived.Type = tbReceivedCurrent.Type;
                    tbRecived.SLNhap = tbReceivedCurrent.SLNhap;
                    tbRecived.SlKiemTra = tbReceivedCurrent.SlKiemTra;
                    tbRecived.Products = tbReceivedCurrent.Products;
                    tbRecived.Time = Time;
                    dbcontext.tbReceiveds.Add(tbRecived);
                    dbcontext.SaveChanges();
                }
            } 
        }
        public bool KetThucDonChuyen(int ReceivedID)
        {
            try
            {
                tbReceived tbrc = dbcontext.tbReceiveds.Find(ReceivedID);
                if(tbrc.ParentId != null)
                {
                    var lstTBRc = dbcontext.tbReceiveds.Where(m => m.ParentId == tbrc.ParentId).ToList();
                    foreach (var item in lstTBRc)
                    {
                        item.DateEnd = DateTime.Now;
                        var time = int.Parse(Math.Round(item.DateEnd.Value.TimeOfDay.TotalSeconds - item.DateStart.TimeOfDay.TotalSeconds).ToString());
                        var kpiTime = tbrc.Time * 60;
                        var kpi = time - kpiTime;
                        item.Kpi = kpi;
                        dbcontext.SaveChanges();
                    }
                }
                if (tbrc.TypeTracking == 1 || tbrc.TypeTracking==2)
                {
                    var lstTBRc = dbcontext.tbReceiveds.Where(m => m.TypeTracking == tbrc.TypeTracking).ToList();
                    foreach(var item in lstTBRc)
                    {
                        item.TypeTracking = tbrc.TypeTracking;
                        item.DateEnd = DateTime.Now;
                        item.Kpi = GetKPI(item.DateStart, DateTime.Now, item.SLNhap.Value, 0, 2);
                        dbcontext.SaveChanges();
                    }
                }
                
                return true;

            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
