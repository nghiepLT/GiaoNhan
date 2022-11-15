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
    public class BAL_ThongKe
    {
        DAL_ThongKe dalThongKe = new DAL_ThongKe();
        public IEnumerable<tbReceived> BaoCaoNhapKhoTheoNgay(int UserID, DateTime fromdate, DateTime todate)
        {
            return dalThongKe.BaoCaoNhapKhoTheoNgay(UserID, fromdate, todate);
        }
        public IEnumerable<tbTracking> BaoCaoXuatKhoTheoNgay(int UserID, DateTime fromdate, DateTime todate)
        {
            return dalThongKe.BaoCaoXuatKhoTheoNgay(UserID, fromdate, todate);
        }

        public IEnumerable<VM_Received> ThongKeNhapChiTiet(DateTime date, int UserID)
        {
            return dalThongKe.ThongKeNhapChiTiet(date, UserID);
        }
        public IEnumerable<VM_Tracking> ThongKeXuatChiTiet(DateTime date, int UserID)
        {
            return dalThongKe.ThongKeXuatChiTiet(date, UserID);
        }
    }
}
