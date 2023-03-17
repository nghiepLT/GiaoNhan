using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.ViewModels;
namespace DAL
{
    public class DAL_SapXep
    {
        public int GetStatusByUserId(int UserID)
        {
            KpiManagerEntities db = new KpiManagerEntities();
            tbSapXepDetail tbdt = db.tbSapXepDetails.ToList().Where(m => m.NgayCapNhat.Value.Date == DateTime.Now.Date).FirstOrDefault();
            if (tbdt != null)
            {
                var getSplit = tbdt.Position.Split(',');
                foreach(var item in getSplit)
                {
                    if (item.Contains(UserID.ToString()))
                    {
                        var getsplit2 = item.Split('_');
                        if (getsplit2[1] == "0")
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
            }
            return 0;
        }
        public IEnumerable<VM_UserSapXep> GetListDSGiaoNhan()
        {
            KpiManagerEntities db = new KpiManagerEntities();
            var model = (from u in db.tbUSers.ToList()
                         select new VM_UserSapXep()
                         {
                             Code=u.Code,
                             UserID=u.UserID,
                             Status=GetStatusByUserId(u.UserID),
                             EmployeeName=u.EmployeeName
                         }

                       ).ToList();
            return model.Where(m => !string.IsNullOrEmpty(m.Code) && m.Code.Contains("GN"));
        }

        public bool SapXepInsertNew(tbSapXepTai tbSapXepTai)
        {
            try
            {
                KpiManagerEntities db = new KpiManagerEntities();
                if (db.tbSapXepTais.Count() == 0)
                {
                    db.tbSapXepTais.Add(tbSapXepTai);
                }
                else
                {
                    var edit = db.tbSapXepTais.FirstOrDefault();
                    edit.DateCreate = DateTime.Now;
                    edit.PositionEmpty = tbSapXepTai.PositionEmpty;
                }
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool SapXepInsert(tbSapXepConfig tbconfig)
        {
            try
            {
                KpiManagerEntities db = new KpiManagerEntities();

                if (db.tbSapXepConfigs.Count() == 0)
                {
                    db.tbSapXepConfigs.Add(tbconfig);
                }
                else
                {
                    var edit = db.tbSapXepConfigs.FirstOrDefault();
                    edit.NgayCapNhat = DateTime.Now;
                    edit.Position = tbconfig.Position;
                    //Cập nhật con
                    tbSapXepDetail sxdt = db.tbSapXepDetails.ToList().Where(m => m.NgayCapNhat.Value.Date == DateTime.Now.Date).LastOrDefault();
                    var getsplit = edit.Position.Split(',');
                    string newPosition = "";
                    foreach (var item in getsplit)
                    {
                        var index = getsplit.ToList().IndexOf(item);
                        int giatri = 0;
                        //Kiem tra
                        var lst2 = sxdt.Position.Split(',').ToList(); 
                        foreach (var item2 in lst2)
                        {
                            giatri = 0;
                            if (item2.Contains(item))
                            {
                                var getsplt2 = item2.Split('_');
                                if (getsplt2[1] == "1")
                                {
                                    giatri = 1;
                                    break;
                                }
                                else
                                {
                                    giatri = 0;
                                }
                            }
                        }
                        if (index < getsplit.Length - 1)
                            newPosition += item + "_" + giatri + ",";
                        else
                            newPosition += item + "_" + giatri;
                    }
                    if (sxdt == null)
                    {
                        tbSapXepDetail tbSapXepDetail = new tbSapXepDetail();
                        tbSapXepDetail.NgayCapNhat = DateTime.Now;
                        tbSapXepDetail.Position = newPosition;
                        db.tbSapXepDetails.Add(tbSapXepDetail);
                    }
                    else
                    {
                        sxdt.NgayCapNhat = DateTime.Now;
                        sxdt.Position = newPosition;
                    }
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public tbSapXepConfig GetLastSapXepconfig()
        {
            KpiManagerEntities db = new KpiManagerEntities();
            return db.tbSapXepConfigs.ToList().LastOrDefault();
        }
        public tbSapXepTai GetLastSapXepTai()
        {
            KpiManagerEntities db = new KpiManagerEntities();
            return db.tbSapXepTais.FirstOrDefault();
        }
        public string GetLastPosition()
        {
            KpiManagerEntities db = new KpiManagerEntities();
            var tbsapxepconfig= db.tbSapXepConfigs.ToList().LastOrDefault();
            var tbsapxepdetail = db.tbSapXepDetails.ToList().Where(m => m.NgayCapNhat.Value.Date == DateTime.Now.Date).FirstOrDefault();
            string Getposition = "";
            if (tbsapxepdetail != null)
            {
                Getposition = tbsapxepdetail.Position;
            }
            else
            {
                var getsplit = tbsapxepconfig.Position.Split(',');
                foreach(var item in getsplit)
                {
                    var index = getsplit.ToList().IndexOf(item);
                    if (index < getsplit.Length - 1)
                        Getposition += item + "_" + "0" + ",";
                    else
                        Getposition += item + "_" + "0";
                }
            }
            return Getposition;
        }
        public IEnumerable<VM_SapXepUser> GetListUSer(string position)
        {
            List<VM_SapXepUser> lstUser = new List<VM_SapXepUser>();
            if (position != null)
            {
                KpiManagerEntities db = new KpiManagerEntities();
                var getSplt = position.Split(',');

                foreach (var item in getSplt)
                {

                    tbUSer tbuser = db.tbUSers.ToList().Where(m => m.UserID.ToString() == item).FirstOrDefault();
                    VM_SapXepUser VM_SapXepUser = new VM_SapXepUser();
                    if (tbuser != null)
                    {
                        VM_SapXepUser.UserID = tbuser.UserID;
                        VM_SapXepUser.Code = tbuser.Code;

                        lstUser.Add(VM_SapXepUser);
                    }

                }
            }
            return lstUser;
        }

        public bool Checkdone(string Mathe)
        {
            KpiManagerEntities db = new KpiManagerEntities();
            var chk = db.tbTheTais.ToList().Any(m => m.DateEnd != null && m.DateCreate.Value.Date == DateTime.Now.Date && m.MaThe==Mathe);
            return chk;
        }

        public bool CreateSapXepDetail()
        {
            KpiManagerEntities db = new KpiManagerEntities();
            tbSapXepDetail tbdt = db.tbSapXepDetails.ToList().Where(m => m.NgayCapNhat.Value.Date == DateTime.Now.Date).FirstOrDefault();
            if (tbdt == null)
            {
                tbSapXepConfig tbcfig = db.tbSapXepConfigs.FirstOrDefault();
                if (tbcfig != null)
                {
                    var position = tbcfig.Position;
                    var getSplit = position.Split(',');
                    string Newposition = "";
                    foreach(var item in getSplit)
                    {
                        var index = getSplit.ToList().IndexOf(item);
                        if (index < getSplit.Length - 1)
                            Newposition += item + "_" + "0" + ",";
                        else
                            Newposition += item + "_" + "0";
                    }
                    tbdt = new tbSapXepDetail();
                    tbdt.Position = Newposition;
                    tbdt.NgayCapNhat = DateTime.Now;
                    db.tbSapXepDetails.Add(tbdt);
                    db.SaveChanges();
                }

            }
            return true;
        }

        public tbSapXepDetail GetSapXepDetail()
        {
            KpiManagerEntities db = new KpiManagerEntities();
            tbSapXepDetail dt = db.tbSapXepDetails.ToList().Where(m => m.NgayCapNhat.Value.Date == DateTime.Now.Date).FirstOrDefault();
            return dt;
        } 
        public bool CapNhatSapXepDetail(int UserID, int Status)
        {
            try
            {
                KpiManagerEntities db = new KpiManagerEntities();
                tbSapXepTai sapxeptai = db.tbSapXepTais.FirstOrDefault();
                var getSplit = sapxeptai.PositionEmpty.Split(',');
                string newPosition = "";
                foreach(var item in getSplit)
                {
                    if (item != UserID.ToString())
                    {
                        newPosition += item + ",";
                    }
                }
                
                if (newPosition.Contains(','))
                    newPosition = newPosition.Substring(0, newPosition.Length - 1);
                sapxeptai.PositionEmpty = newPosition;
                if (sapxeptai.PositionDone == null)
                {
                    sapxeptai.PositionDone += UserID.ToString();
                }
                else
                {
                    sapxeptai.PositionDone +=","+ UserID.ToString();
                }
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public tbUSer GetUserIdByCode(string Code)
        {
            KpiManagerEntities db = new KpiManagerEntities();
            return db.tbUSers.Where(m => m.Code == Code).FirstOrDefault();
        }

        public string GetUSerByPosition(string position)
        {
            KpiManagerEntities db = new KpiManagerEntities();
            string result = "";
            var getSplit = position.Split(',');
            string newPosition = "";
            foreach(var item in getSplit)
            {
                var getsplit2 = item.Split('_');
                tbUSer tuUser = db.tbUSers.ToList().Where(m => m.UserID.ToString() == getsplit2[0]).FirstOrDefault();
                if (tuUser != null)
                    newPosition += tuUser.Code + "_";

            }
            result = newPosition;
            return result;
        }
        public IEnumerable<tbSapXepDetail> GetListSapXepDetail()
        {
            KpiManagerEntities db = new KpiManagerEntities();
            var model = (from dt in db.tbSapXepDetails.ToList()
                         select new tbSapXepDetail()
                         {
                             NgayCapNhat=dt.NgayCapNhat,
                             Position=GetUSerByPosition(dt.Position)
                         }
                       ).OrderByDescending(m=>m.NgayCapNhat);
            return model;
        }

        public bool KiemTraTheHoatDong(string Mathe)
        {
            KpiManagerEntities db = new KpiManagerEntities();
            tbSapXepDetail tbsapdt = db.tbSapXepDetails.ToList().Where(m => m.NgayCapNhat.Value.Date == DateTime.Now.Date).FirstOrDefault();
            if (tbsapdt != null)
            {
                var position = tbsapdt.Position;
                var getSplit = position.Split(',');
                foreach(var item in getSplit)
                {
                    var splt2 = item.Split('_');
                    if(splt2[0]==Mathe && splt2[1]=="1")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool SwapCode(string FirstCode,string TwoCode)
        {
            KpiManagerEntities db = new KpiManagerEntities();
            var tbsapdt = db.tbSapXepTais.FirstOrDefault();
            if (tbsapdt == null)
                return false;
            string position = tbsapdt.PositionEmpty;
            var getSplit = position.Split(',');
            int firstIndex = 0;
            string firstValue = "";
            int twoIndex = 0;
            string twoValue = "";

            foreach(var item in getSplit)
            {
                if (item.Contains(FirstCode))
                {
                    firstIndex = getSplit.ToList().IndexOf(item);
                    firstValue = item;
                }
                if (item.Contains(TwoCode))
                {
                    twoIndex = getSplit.ToList().IndexOf(item);
                    twoValue = item;
                }
               
            }
            getSplit[firstIndex] = twoValue;
            getSplit[twoIndex] = firstValue;
            string newposition = "";
            foreach (var item in getSplit)
            {
                var indexof = getSplit.ToList().IndexOf(item);
                if (indexof < getSplit.Length - 1)
                    newposition += item + ",";
                else
                    newposition += item;
            }
              tbsapdt.PositionEmpty = newposition;

            //Sap xep con
            db.SaveChanges();
            return true;
        }
    }
}
