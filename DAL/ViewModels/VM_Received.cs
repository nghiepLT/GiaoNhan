using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class VM_ReceivedListTotal
    {
        public DateTime DateCreate { get; set; }
        public int? Totals { get; set; }
        public int TotalRow { get; set; }
        public int KPI { get; set; }
        public string KPIResults { get; set; } 
        public int ToTalTimes { get; set; }
        public string TimesResult { get; set; }
    }
    public class VM_ReceivedList
    {
        public DateTime DateCreate { get; set; }
        public int ? Totals { get; set; }
        public int TotalRow { get; set; }
        public int KPI { get; set; }
        public string KPIResults { get; set; }
        public int ToTalTimes { get; set; }
        public string TimesResult { get; set; }

        public List<VM_Received> lstVMReceived = new List<VM_Received>();
    }
   public class VM_Received
    {
        public int ReceivedID { get; set; }
        public int ? STT { get; set; }
        public DateTime DateStart { get; set; }

        public DateTime ? DateEnd { get; set; }
        public int ? Type { get; set; }
        public int ? ToTals { get; set; }
        public int  KPI { get; set; }
        public string ProductDescription { get; set; }
        public string KPIResults { get; set; }
        public int ToTalTimes { get; set; }
        public string TimesResult { get; set; }

    }
}
