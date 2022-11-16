using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.ViewModels;
using Newtonsoft.Json;
using System.Net;

namespace DAL
{
    public class DAL_TheTai
    {
        DAL_Config balConfig = new DAL_Config();
        KpiManagerEntities dbContext = new KpiManagerEntities();
        public tbTheTai CheckExist(string MaThe,DateTime now)
        {
            return dbContext.tbTheTais.OrderByDescending(m => m.ThetaiID).ToList().Where(m => m.MaThe == MaThe && m.DateStart.Value.Date==now.Date).FirstOrDefault();
        }
        public bool InsertData(tbTheTai tbTheTai)
        {

            dbContext.tbTheTais.Add(tbTheTai);
            return dbContext.SaveChanges() == 1 ? true : false;
        }
        public bool UpdateData(tbTheTai tbTheTai)
        {
            try
            {
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool UpdateDataDetail(tbTheTaiChiTiet tbThetaichitiet)
        {
            try
            {
                if (tbThetaichitiet.TheTaiChiTietID == 0)
                    dbContext.tbTheTaiChiTiets.Add(tbThetaichitiet);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public tbTheTai GetLastTheTai(string MaThe)
        {
            return dbContext.tbTheTais.OrderByDescending(m => m.ThetaiID).Where(m=>m.MaThe== MaThe).FirstOrDefault();
        }
        public bool UpdatePhieuXuat(int ThetaiID, string MaPhieu)
        {
            try
            {
                tbTheTai _tbTheTai = dbContext.tbTheTais.Find(ThetaiID);
                if (_tbTheTai != null)
                {
                    _tbTheTai.MaPhieu = MaPhieu;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }
        public IEnumerable<VM_TheTai> LoadTracking(int UserID)
        {
            var model = (from m in dbContext.tbTheTais.ToList()
                         where ((UserID == 2029 || UserID == 1019) || m.UserId == UserID) && (m.DateStart.Value.Date == DateTime.Now.Date)
                         select new VM_TheTai()
                         {
                             ThetaiID=m.ThetaiID,
                             MaPhieu=m.MaPhieu,
                             DateEnd = m.DateEnd,
                             DateStart = m.DateStart.Value,
                             KPI = m.KPI != null ? m.KPI.Value * (-1) : 0,
                             Count = m.Count,
                             MaThe = m.MaThe, 
                             ToTalTimes = m.DateEnd != null ? int.Parse(Math.Round(double.Parse((m.DateEnd.Value.TimeOfDay.TotalSeconds - m.DateStart.Value.TimeOfDay.TotalSeconds).ToString())).ToString()) : 0,
                             TimesResult = m.DateEnd != null ? Tool.Helper.ReturnTime(int.Parse(Math.Round(double.Parse((m.DateEnd.Value.TimeOfDay.TotalSeconds - m.DateStart.Value.TimeOfDay.TotalSeconds).ToString())).ToString())) : ""
                         }
                       ).OrderByDescending(m => m.DateStart);
            return model;
        }
        public bool Checkparent(string MaThe)
        {
            return dbContext.tbTheTais.OrderByDescending(m => m.ThetaiID).Any(m => m.MaThe == MaThe && m.DateEnd == null);
        }
        public IEnumerable<tbTheTaiChiTiet> GetAlltheTaiChiTiet(string MaThe)
        {  
            return dbContext.tbTheTaiChiTiets.ToList().Where(m=>m.DateEnd==null || (m.DateEnd.HasValue && m.DateEnd.Value.Date == DateTime.Now.Date)).Where(m => m.MaThe == MaThe && !Checkparent(m.MaThe)).OrderByDescending(m=>m.TheTaiChiTietID);
        }
        public bool UpdateStatus(int TheTaiChiTietID)
        {
            try
            {
                tbTheTaiChiTiet tcct = dbContext.tbTheTaiChiTiets.Where(m => m.TheTaiChiTietID == TheTaiChiTietID).FirstOrDefault();
                if (tcct != null)
                {
                    tcct.Status = 1;
                    tcct.DateEnd = DateTime.Now;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public string GetGroupTheTai()
        {
            string strResult = "";
            var lst = dbContext.tbTheTais.ToList().Where(m => m.DateStart.Value.Date == DateTime.Now.Date);
            if (lst != null && lst.Count()>0)
            {
                var gr = lst.GroupBy(m => m.MaThe);
                foreach (var item in gr)
                {
                    strResult += item.Key + ",";
                }
            }
           
            return strResult;
        }
        public string GetGroupTheTaiwait() //n2fix
        {
            string strResult = "";
            var lst = dbContext.tbTheTais.ToList().Where(m => m.DateEnd == null && m.DateStart.Value.Date == DateTime.Now.Date);
            if (lst != null && lst.Count() > 0)
            {
                var gr = lst.GroupBy(m => m.MaThe);
                foreach (var item in gr)
                {
                    strResult += item.Key + ",";
                }
            }

            return strResult;
        }
        public bool CheckDetailThetai(string MaPhieu)
        {
            return dbContext.tbTheTaiChiTiets.Any(m => m.MaPhieu == MaPhieu && m.Status==1);
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
        public bool CheckMaHopLe(string MaThe)
        {

            //Kiểm tra thẻ tài
            bool check = true;
            if (MaThe.Contains("GN"))
            {
                check = dbContext.tbUSers.Any(m => m.Code == MaThe);
                if (check == true)
                {
                    return true;
                }
                else
                    return false;
            }
            if (MaThe.Contains("-L"))
            {
                var getJson = GetJsonData(MaThe);
                check = getJson != null ? true : false;
                if (check == true)
                {
                    return true;
                }
                else
                    return false;
            }

            return false ;
        }

        public bool UpdateCancle(int TheTaiChiTietID, string Description)
        {
            try
            {
                tbTheTaiChiTiet ttct = dbContext.tbTheTaiChiTiets.Find(TheTaiChiTietID);
                ttct.Status = -1;
                ttct.Description = Description;
                ttct.DateEnd = DateTime.Now;
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
