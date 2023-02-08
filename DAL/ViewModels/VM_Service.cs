using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
   public class VM_Service
    {
        public int IdService { get; set; }
        public string NameService { get; set; }
        public int IDCar { get; set; }
        public string CarSignal { get; set; }
        public DateTime Ngaytao { get; set; }
        public double TongTien { get; set; }
        public int Type { get; set; }
        public string TenTaiXe { get; set; }
        public string Ghichu { get; set; }
    }
}
