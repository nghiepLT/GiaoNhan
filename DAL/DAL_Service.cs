using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Tool;
using DAL.ViewModels;
namespace DAL
{
   public class DAL_Service
    {
        KpiManagerEntities dbContext = new KpiManagerEntities();

        public IEnumerable<VM_Service> GetListService()
        {
            var model = (from sv in dbContext.bdServices
                         join c in dbContext.bdCars
                         on sv.IdCar equals c.IDCar
                         join tx in dbContext.bdTaixes
                         on c.IdTaiXe equals tx.IdTaixe
                         select new VM_Service()
                         {
                             CarSignal=c.CarSignal,
                             IDCar=c.IDCar,
                             IdService=sv.IdService,
                             NameService=sv.NameService,
                             Ngaytao=sv.Ngaytao.Value,
                             TongTien=sv.TongTien.Value,
                             Type=sv.Type.Value,
                             TenTaiXe=tx.TenTaiXe,
                             Ghichu=sv.Ghichu
                         }
                       ).OrderByDescending(m=>m.IdService).ToList();
            return model;
        }
        public bdService GetServiceByID(int IdService)
        {
            return dbContext.bdServices.Find(IdService);
        }
        public bool InsertService(bdService bdService)
        {
            try
            {
                if (bdService.IdService == 0)
                {
                    dbContext.bdServices.Add(bdService);
                    dbContext.SaveChanges();
                }
                else
                {
                    bdService bdedit = dbContext.bdServices.Find(bdService.IdService);
                    bdedit.NameService = bdService.NameService;
                    bdedit.IdCar = bdService.IdCar;
                    bdedit.TongTien = bdService.TongTien;
                    bdedit.Type = bdService.Type;
                    bdedit.Ghichu = bdService.Ghichu;
                    bdedit.Ngaytao = bdService.Ngaytao;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteService(int IdService)
        {
            try
            {
                var item = dbContext.bdServices.Find(IdService);
                dbContext.bdServices.Remove(item);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
