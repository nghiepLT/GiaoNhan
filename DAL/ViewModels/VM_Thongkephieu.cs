using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class VM_Thongkephieu
    {
        public string MaPhieu { get; set; }
        public DateTime Thoigiankinhdoanhxuatphieu { get; set; }
        public DateTime ThoigianBatdauXuatphieu { get; set; }
        public DateTime ThoigianKetthucXuatphieu { get; set; }
        public DateTime Batdaulamhang { get; set; }
        public DateTime Ketthuclamhang { get; set; }
        public DateTime Thoigiangiaohang { get; set; }
    }
}
