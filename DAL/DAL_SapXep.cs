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
        public IEnumerable<tbUSer> GetListDSGiaoNhan()
        {
            KpiManagerEntities db = new KpiManagerEntities();

            return db.tbUSers.Where(m => !string.IsNullOrEmpty(m.Code) && m.Code.Contains("GN"));
        }

        public bool SapXepInsert(tbSapXepConfig tbconfig)
        {
            try
            {
                KpiManagerEntities db = new KpiManagerEntities();
                db.tbSapXepConfigs.Add(tbconfig);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                
            }
            return false;
        }

        public tbSapXepConfig GetLastSapXepconfig()
        {
            KpiManagerEntities db = new KpiManagerEntities();
            return db.tbSapXepConfigs.ToList().LastOrDefault();
        }
        public IEnumerable<VM_SapXepUser> GetListUSer(string position)
        {
            KpiManagerEntities db = new KpiManagerEntities();
            var getSplt = position.Split(',');
            List<VM_SapXepUser> lstUser = new List<VM_SapXepUser>();
            foreach(var item in getSplt)
            {
                tbUSer tbuser = db.tbUSers.ToList().Where(m => m.UserID.ToString() == item).FirstOrDefault();
                VM_SapXepUser VM_SapXepUser = new VM_SapXepUser();
                VM_SapXepUser.UserID = tbuser.UserID;
                VM_SapXepUser.Code = tbuser.Code;
                //Kiểm tra the tài
                var checkthetai = db.tbTheTais.Where(m => m.MaThe == tbuser.Code && m.DateEnd != null).FirstOrDefault();
                if (checkthetai != null)
                    VM_SapXepUser.Status = 1;
                else
                    VM_SapXepUser.Status = 0;
                lstUser.Add(VM_SapXepUser);
            }
            return lstUser;
        }

        public bool Checkdone(string Mathe)
        {
            KpiManagerEntities db = new KpiManagerEntities();
            var chk = db.tbTheTais.ToList().Any(m => m.DateEnd != null && m.DateCreate.Value.Date == DateTime.Now.Date && m.MaThe==Mathe);
            return chk;
        }
    }
}
