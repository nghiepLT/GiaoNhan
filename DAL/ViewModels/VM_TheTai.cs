using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class VM_TheTai
    {
        public int ThetaiID { get; set; }
        public string MaThe { get; set; }
        public string MaPhieu { get; set; }
        public DateTime DateStart { get; set; }

        public DateTime? DateEnd { get; set; }
        public int? Count { get; set; }
        public int? KPI { get; set; } 
        public int ToTalTimes { get; set; }
        public string TimesResult { get; set; }
    }
}
