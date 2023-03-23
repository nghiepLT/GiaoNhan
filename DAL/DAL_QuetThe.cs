using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.ViewModels;
namespace DAL
{
   public class DAL_QuetThe
    {
        public bool CheckQuetThe(string TrackingCode)
        {
            KpiManagerEntities dbContext = new KpiManagerEntities();
            
            return dbContext.tbCLTrackings.Any(m=>m.TrackingCode==TrackingCode.Trim());
        }
        public int InsertQuetThe(string TrackingCode,int UserId)
        {
           
            try
            {
                KpiManagerEntities dbContext = new KpiManagerEntities();
                tbCLTracking tbCL = new tbCLTracking();
                tbCL.TrackingCode = TrackingCode;
                tbCL.TrackingDate = DateTime.Now;
                tbCL.UserId = UserId;
                dbContext.tbCLTrackings.Add(tbCL);
                dbContext.SaveChanges();
                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        public IEnumerable<tbCLTracking> GetListQuetThe()
        {
            KpiManagerEntities dbContext = new KpiManagerEntities();
            return dbContext.tbCLTrackings.ToList().Where(m=>m.TrackingDate.Value.Date==DateTime.Now.Date).OrderByDescending(m=>m.CLTrackingId);
        }
        public IEnumerable<tbCLTracking> GetListQuetTheReport(DateTime fromDate,DateTime toDate,string TrackingCode)
        {
            KpiManagerEntities dbContext = new KpiManagerEntities();
            if(!string.IsNullOrEmpty(TrackingCode))
                return dbContext.tbCLTrackings.ToList().Where(m=>TrackingCode != null && TrackingCode == m.TrackingCode).OrderByDescending(m => m.CLTrackingId);
            return dbContext.tbCLTrackings.ToList().Where(m => (m.TrackingDate.Value.Date >= fromDate.Date && m.TrackingDate.Value.Date <= toDate.Date)).OrderByDescending(m => m.CLTrackingId);
        }
    }
}
