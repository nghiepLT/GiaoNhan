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
        public IEnumerable<VM_Thongkephieu> Thongkephieu(DateTime fromDate, DateTime toDate,int Status)
        {
            return dalThongKe.Thongkephieu(fromDate, toDate, Status);
        }
        public IEnumerable<VM_Service> ReportChiPhiDichVu(DateTime fromDate, DateTime toDate, int Type, int typeDate, int month,int Idcar)
        {
            return dalThongKe.ReportChiPhiDichVu(fromDate, toDate, Type, typeDate, month, Idcar);
        }
        public IEnumerable<VM_DotBaoDuong> ReportDotbaoduong(int Idcar)
        {
            return dalThongKe.ReportDotbaoduong(Idcar);
        }
    }
}
