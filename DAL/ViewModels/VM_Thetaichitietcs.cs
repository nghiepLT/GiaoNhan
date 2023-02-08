using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class VM_Thetaichitietcs
    {
        public int TheTaiChiTietID { get; set; }
        public Nullable<int> ThetaiID { get; set; }
        public string MaPhieu { get; set; }
        public Nullable<int> Status { get; set; }
        public string MaThe { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
