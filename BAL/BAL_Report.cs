using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using DAL.ViewModels;
namespace BAL
{
    public class BAL_Report
    {
        Dal_Report dalReport = new Dal_Report();
        public IEnumerable<VM_Tracking> ReportTrackingDetail(DateTime date, int UserID)
        {
            return dalReport.ReportTrackingDetail(date, UserID);
        }
        public IEnumerable<VM_Received> ReportReceivedDetail(DateTime date, int UserID)
        {
            return dalReport.ReportReceivedDetail(date, UserID);
        }
        public IEnumerable<VM_TheTaiReport> ReportGiaoNhan(DateTime fd, DateTime td,string MaThe)
        {
            return dalReport.ReportGiaoNhan(fd, td, MaThe);
        }

       
    }
}
