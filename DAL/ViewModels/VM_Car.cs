using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class VM_Car
    {
        public int IDCar { get; set; }
        public string CarSignal { get; set;}
        public string TenTaiXe { get; set; }
        public string MaTheTai { get; set; }
        public string NameType { get; set; }
        public int DinhMucBaoDuong { get; set; }
        public DateTime ThoiGianDangKiem { get; set; }
        public int SoKmHientai { get; set; }
    }
}
