using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class VM_TrackingListTotal
    {
        public DateTime DateCreate { get; set; }
        public int? Totals { get; set; }
        public int TotalRow { get; set; }
        public int CountStep { get; set; }
        public int KPI { get; set; }
        public string KPIResults { get; set; }
        public int ToTalTimes { get; set; }
        public string TimesResult { get; set; }
    }
    public class VM_TrackingList
    {
        public DateTime DateCreate { get; set; }
        public int? Totals { get; set; }
        public int CountStep { get; set; }
        public int TotalRow { get; set; }
        public int KPI { get; set; }
        public string KPIResults { get; set; }
        public int ToTalTimes { get; set; }
        public string TimesResult { get; set; }
        public List<VM_Tracking> lstVMTracking = new List<VM_Tracking>();
    }
    public class VM_Tracking
    {
        public string TrackingCode { get; set; }
        public int TrackingID { get; set; } 
        public DateTime DateStart { get; set; }

        public DateTime? DateEnd { get; set; } 
        public int? ToTals { get; set; }
        public int CountStep { get; set; }
        public int KPI { get; set; } 
        public string KPIResults { get; set; }
        public int ToTalTimes { get; set; }
        public string TimesResult { get; set; }
    }
}
