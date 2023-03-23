using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class BAL_QuetThe
    {
        DAL_QuetThe dalQuetThe = new DAL_QuetThe();
        public bool CheckQuetThe(string TrackingCode)
        {
            return dalQuetThe.CheckQuetThe(TrackingCode);
        }
        public int InsertQuetThe(string TrackingCode, int UserId)
        {
            return dalQuetThe.InsertQuetThe(TrackingCode, UserId);
        }
        public IEnumerable<tbCLTracking> GetListQuetThe()
        {
            return dalQuetThe.GetListQuetThe();
        }
        public IEnumerable<tbCLTracking> GetListQuetTheReport(DateTime fromDate, DateTime toDate, string TrackingCode)
        {
            return dalQuetThe.GetListQuetTheReport(fromDate, toDate, TrackingCode);
        }
    }
}
