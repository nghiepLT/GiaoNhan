using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class VM_DotBaoDuong
    {
        public int IdDotBaoDuong { get; set; }
        public Nullable<int> IDCar { get; set; }
        public Nullable<System.DateTime> NgayBD { get; set; }
        public Nullable<System.DateTime> NgayKT { get; set; }
        public Nullable<int> SokmDau { get; set; }
        public Nullable<int> SoKmCuoi { get; set; }
        public Nullable<int> SoKmHientai { get; set; }
        public int STT { get; set; }
        public int DinhMucBaoDuong { get; set; }
        public int Chisodukienlansau { get; set; }
        public int Sokmconlai { get; set; }
        public string CarSignal { get; set; }
        public string TenTaiXe { get; set; }
    }
}
