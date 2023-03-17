using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.ViewModels;
using Newtonsoft.Json;
using System.Net;
using System.IO;

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
                             TimesResult = m.DateEnd != null ? Tool.Helper.ReturnTime(int.Parse(Math.Round(double.Parse((m.DateEnd.Value.TimeOfDay.TotalSeconds - m.DateStart.Value.TimeOfDay.TotalSeconds).ToString())).ToString())) : "",
                             Luotdi=m.Luotdi.HasValue? m.Luotdi.Value:DateTime.MinValue,
                             Luotve=m.Luotve.HasValue?m.Luotve.Value:DateTime.MinValue
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
           // var model1 = dbContext.tbTheTaiChiTiets.ToList().Where(m=>m.MaThe == MaThe && (m.DateCreate.HasValue && m.DateCreate.Value.Date == DateTime.Now.Date)).OrderByDescending(m => m.TheTaiChiTietID).ToList();
            var model1 = dbContext.tbTheTaiChiTiets.ToList().Where(m=>m.MaThe == MaThe && (m.DateCreate.HasValue && m.DateCreate.Value.Date == DateTime.Now.Date || (m.DateCreate.HasValue && m.DateCreate.Value.Date >= DateTime.Now.Date.AddDays(-1) && m.DateEnd == null))).OrderByDescending(m => m.TheTaiChiTietID).ToList();
            //var model2= model1.Where(m =>!Checkparent(m.MaThe)).OrderByDescending(m => m.TheTaiChiTietID);
            return model1;
        }
        public string GetUserthetaichitiet(int TheTaiChiTietID)
        {
            tbTheTaiChiTiet ttct = dbContext.tbTheTaiChiTiets.Where(m => m.TheTaiChiTietID == TheTaiChiTietID).FirstOrDefault();
            var thetaiid = ttct.ThetaiID;
            var userid = dbContext.tbTheTais.Where(m => m.ThetaiID == thetaiid).FirstOrDefault().MaThe;
            return dbContext.tbUSers.Where(m => m.Code == userid).FirstOrDefault().UserName;
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

        public bool UpdateCancle(int TheTaiChiTietID, string Description,int Type, int? TienPhatSinh, int? SoKMPhatSinh,int ? NhanTienMat )
        {
            try
            {
                tbTheTaiChiTiet ttct = dbContext.tbTheTaiChiTiets.Find(TheTaiChiTietID);
                if (Type == 1)
                {
                    ttct.Status = 1;
                }
                else
                {
                    ttct.Status = -1; 
                } 
                ttct.Description = Description;
                ttct.DateEnd = DateTime.Now;
                ttct.TienPhatSinh = TienPhatSinh.Value;
                ttct.SoKMPhatSinh = SoKMPhatSinh.Value;
                ttct.NhanTienMat = NhanTienMat.Value;
                dbContext.SaveChanges();
                //
                string querySoPhieu = "";
                string querySoTien = "";
                string querySoKM = "";
                string queryNotePhieu = "";
                string queryTienMat = "";
                querySoPhieu = "sopx=" + ttct.MaPhieu;
                if (ttct.TienPhatSinh != 0)
                    querySoTien = "&sotien=" + ttct.TienPhatSinh;
                if (ttct.SoKMPhatSinh != 0)
                    querySoKM = "&sokm=" + ttct.SoKMPhatSinh;
                if (ttct.Description != "")
                    queryNotePhieu = "&notephieu=" + ttct.Description;
                if (ttct.NhanTienMat != 0)
                    queryTienMat = "&tienmat=" + 1;
                if (ttct.TienPhatSinh!=0 || ttct.SoKMPhatSinh!=0 || ttct.Description != "" || ttct.NhanTienMat!=0)
                {
                     var url = "http://192.168.117.214:8008/jsphieuve.asp?" + querySoPhieu+querySoTien+querySoKM+queryNotePhieu+queryTienMat;
                   // var url = "http://192.168.117.117/jsphieuve.asp?" + querySoPhieu.Trim() + querySoTien.Trim() + querySoKM.Trim() + queryNotePhieu.Trim() + queryTienMat.Trim();

                    // WebRequest request = HttpWebRequest.Create(url);
                    HttpWebRequest webRequest =
    WebRequest.Create(url) as HttpWebRequest;

                    webRequest.Credentials = CredentialCache.DefaultCredentials;

                    HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse;
                }
              
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool CapnhatLuotDi(int ThetaiID)
        {
            try
            {
                var tbthetai = dbContext.tbTheTais.Find(ThetaiID);
                tbthetai.Luotdi = DateTime.Now;
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {

            }
            return false;
        }
        public bool CapnhatLuotVe(string  Code)
        {
            try
            {
                tbTheTai gettbthetai = dbContext.tbTheTais.ToList().Where(m => m.MaThe == Code).LastOrDefault();
                var tbthetai = dbContext.tbTheTais.Find(gettbthetai.ThetaiID); 
                tbthetai.Luotve = DateTime.Now;
                tbUSer tbuser = dbContext.tbUSers.Where(m => m.Code == Code).FirstOrDefault();
                //Cap nhật position
                tbSapXepTai sapxeptai = dbContext.tbSapXepTais.FirstOrDefault();
                var getSplit = sapxeptai.PositionDone.Split(',');
                string newPostionDone = "";
                foreach (var item in getSplit)
                {
                    if (item != tbuser.UserID.ToString())
                    {
                        newPostionDone += item + ",";
                    }
                }
                if (newPostionDone != "")
                    newPostionDone = newPostionDone.Substring(0, newPostionDone.Length - 1);
                sapxeptai.PositionDone = newPostionDone;
                if (sapxeptai.PositionEmpty != "")
                    sapxeptai.PositionEmpty += "," + tbuser.UserID.ToString();
                else
                    sapxeptai.PositionEmpty +=tbuser.UserID.ToString();
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public bool KiemTraGiaoHetPhieu(string Code)
        {
            tbTheTai tbuser = dbContext.tbTheTais.ToList().Where(m => m.MaThe == Code).LastOrDefault();
            if (tbuser != null)
            {
                if (tbuser.Luotve != null)
                    return true;
                var check = dbContext.tbTheTaiChiTiets.Where(m => m.ThetaiID == tbuser.ThetaiID).Any(m => m.DateEnd == null);
                if (check)
                    return true;
            }
            else
            {
                return true;
            }
            return false;
        }

        public bool KiemTraKetThucLuot(string MaThe)
        {
            tbTheTaiChiTiet ttct = dbContext.tbTheTaiChiTiets.ToList().Where(m => m.MaThe == MaThe).LastOrDefault();
            if (ttct == null)
                return false;
            if (ttct.DateEnd == null)
                return true;
            return false;
        }
    }
}
