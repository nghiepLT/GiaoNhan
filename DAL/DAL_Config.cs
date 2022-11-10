using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Models;
using DAL.ViewModels;
namespace DAL
{
    public class DAL_Config
    {
        KpiManagerEntities dbcontext = new KpiManagerEntities();
        public tbConfig Getconfig()
        {
            return dbcontext.tbConfigs.FirstOrDefault();
        }
        public bool UpdateConfig(tbConfig tbConfig)
        {
            try
            {
                tbConfig _tbConfig = dbcontext.tbConfigs.Find(tbConfig.ConfigID);
                _tbConfig.TG40 = tbConfig.TG40;
                tbConfig.TG80 = tbConfig.TG80;
                tbConfig.TimeTracking = tbConfig.TimeTracking;
                tbConfig.TimeDelivery = tbConfig.TimeDelivery;
                dbcontext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
