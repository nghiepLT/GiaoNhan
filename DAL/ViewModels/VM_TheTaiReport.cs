using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
namespace DAL.ViewModels
{
    public class VM_TheTaiReport
    {
        public int ThetaiID { get; set; }
        public string MaThe { get; set; }
        public string MaPhieu { get; set; } 
        public Nullable<System.DateTime> DateStart { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<int> UserId { get; set; }
        public int ToTalTimes { get; set; }
        public string TongThoigian { get; set; }
        public DateTime Luotdi { get; set; }
        public DateTime Luotve { get; set; }
        public List<tbTheTaiChiTiet> lstTheTaiChiTiet { get; set; } = new List<tbTheTaiChiTiet>();
    } 
}
