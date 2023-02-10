using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.ViewModels;
using Newtonsoft.Json;
using System.Net;
using System.Globalization;

namespace DAL
{
    public class DAL_ThongKe
    {
        KpiManagerEntities dbcontext = new KpiManagerEntities();
        public IEnumerable<tbReceived> BaoCaoNhapKhoTheoNgay(int UserID, DateTime fromdate, DateTime todate)
        {
            var model = dbcontext.tbReceiveds.ToList().Where(m => m.UserID == UserID && (m.DateStart.Date >= fromdate.Date && m.DateStart.Date <= todate.Date)).ToList();
            return model;
        }
        public IEnumerable<tbTracking> BaoCaoXuatKhoTheoNgay(int UserID, DateTime fromdate, DateTime todate)
        {
            var model = dbcontext.tbTrackings.ToList().Where(m => m.UserID == UserID && (m.DateStart.Value.Date >= fromdate.Date && m.DateStart.Value.Date <= todate.Date)).ToList();
            return model;
        }

        public IEnumerable<VM_Received> ThongKeNhapChiTiet(DateTime date, int UserID)
        {
            var model = dbcontext.tbReceiveds.ToList().Where(m => m.DateStart.Date == date.Date && m.UserID == UserID);
            var result = (from n in model
                          select new VM_Received()
                          {
                              DateEnd = n.DateEnd,
                              DateStart = n.DateStart,
                              KPI = n.Kpi.HasValue ? n.Kpi.Value * (-1) : 0,
                              Products = n.Products,
                              Type = n.Type,
                              ToTals = (n.SLNhap != null && n.SLNhap > 0) ? n.SLNhap : n.SlKiemTra,
                              ToTalTimes = n.DateEnd != null ? int.Parse(Math.Round(double.Parse((n.DateEnd.Value.TimeOfDay.TotalSeconds - n.DateStart.TimeOfDay.TotalSeconds).ToString())).ToString()) : 0,
                              TimesResult = n.DateEnd != null ? Tool.Helper.ReturnTime(int.Parse(Math.Round(double.Parse((n.DateEnd.Value.TimeOfDay.TotalSeconds - n.DateStart.TimeOfDay.TotalSeconds).ToString())).ToString())) : ""
                          });
            return result;
        }
        public IEnumerable<VM_Tracking> ThongKeXuatChiTiet(DateTime date, int UserID)
        {
            var model = dbcontext.tbTrackings.ToList().Where(m => m.DateStart.Value.Date == date.Date && m.UserID == UserID);
            var result = (from n in model
                          select new VM_Tracking()
                          {
                              DateEnd = n.DateEnd,
                              DateStart = n.DateStart.Value,
                              KPI = n.KPI.HasValue ? n.KPI.Value * (-1) : 0,
                              ToTals = n.CountStep,
                              TrackingCode = n.TrackingCode,
                              ToTalTimes = n.DateEnd != null ? int.Parse(Math.Round(double.Parse((n.DateEnd.Value.TimeOfDay.TotalSeconds - n.DateStart.Value.TimeOfDay.TotalSeconds).ToString())).ToString()) : 0,
                              TimesResult = n.DateEnd != null ? Tool.Helper.ReturnTime(int.Parse(Math.Round(double.Parse((n.DateEnd.Value.TimeOfDay.TotalSeconds - n.DateStart.Value.TimeOfDay.TotalSeconds).ToString())).ToString())) : ""
                          });
            return result;
        }
        public DateTime Giobd(string Maphieu)
        {
            DateTime datereturn = new DateTime();
            var model = dbcontext.tbTheTais.Where(m => m.MaPhieu.Contains(Maphieu)).FirstOrDefault();
            if (model != null)
                return model.DateStart != null ? model.DateStart.Value : DateTime.MinValue;
            return datereturn;
        }
        public DateTime Giokt(string Maphieu)
        {
            DateTime datereturn = new DateTime();
            var model = dbcontext.tbTheTais.Where(m => m.MaPhieu.Contains(Maphieu)).FirstOrDefault();
            if (model != null)
                return model.DateEnd != null ? model.DateEnd.Value : DateTime.MinValue;
            return datereturn;
        }
        public DateTime Thoigiangiaohang(int ThetaiID ,string Maphieu)
        {
            DateTime datereturn = new DateTime();
            var model = dbcontext.tbTheTaiChiTiets.Where(m => m.ThetaiID == ThetaiID && m.MaPhieu==Maphieu).FirstOrDefault();
            if (model != null)
                return model.DateEnd.HasValue ? model.DateEnd.Value : DateTime.MinValue;
            return datereturn;
        }
        public int GetThetaiID(string Maphieu)
        {
            if(Maphieu== "X221214006-L")
            {
                int a = 10;
            }
            var model = dbcontext.tbTheTais.Where(m => m.MaPhieu.Contains(Maphieu)).FirstOrDefault();
            if (model != null)
                return model.ThetaiID;
            return 0;
        }
        public IEnumerable<VM_Thongkephieu> Thongkephieu(DateTime fromDate, DateTime toDate,int Status)
        {
            var model = (from tk in dbcontext.tbTrackings.ToList()
                         where tk.DateCreate.Value.Date >= fromDate.Date && tk.DateCreate.Value.Date <= toDate.Date
                         select new VM_Thongkephieu()
                         {
                             MaPhieu = tk.TrackingCode,
                             Thoigiankinhdoanhxuatphieu = tk.DateCreate.Value,
                             ThoigianBatdauXuatphieu = tk.DateStart.Value,
                             ThoigianKetthucXuatphieu = tk.DateEnd != null ? tk.DateEnd.Value : DateTime.MinValue,
                             Batdaulamhang = Giobd(tk.TrackingCode),
                             Ketthuclamhang = Giokt(tk.TrackingCode),
                             Thoigiangiaohang= Thoigiangiaohang(GetThetaiID(tk.TrackingCode), tk.TrackingCode)
                         }
                       );
            if (Status == 1)
            {
                model = model.Where(m => m.Thoigiangiaohang != DateTime.MinValue).ToList();
            }
            if (Status == 2)
            {
                model = model.Where(m => m.Thoigiangiaohang == DateTime.MinValue).ToList();
            }
            return model;
        }


        #region Thống kê dịch vụ
        public IEnumerable<VM_Service> ReportChiPhiDichVu(DateTime fromDate, DateTime toDate, int Type, int typeDate, int month,int Idcar)
        {
            List<bdService> modelservice = new List<bdService>();
            if (typeDate == 2)
                modelservice = dbcontext.bdServices.ToList().Where(m => (m.Ngaytao.Value.Date >= fromDate.Date && m.Ngaytao.Value.Date <= toDate.Date)).ToList();
            else
                modelservice = dbcontext.bdServices.ToList().Where(m => m.Ngaytao.Value.Month == month && m.Ngaytao.Value.Year == DateTime.Now.Year).ToList();
            var model = (from sv in modelservice
                         join c in dbcontext.bdCars
                         on sv.IdCar equals c.IDCar
                         join tx in dbcontext.bdTaixes
                         on c.IdTaiXe equals tx.IdTaixe
                         where (Type == 0 || sv.Type == Type)
                         && (Idcar==0 || c.IDCar == Idcar)
                         select new VM_Service()
                         {
                             CarSignal = c.CarSignal,
                             IDCar = c.IDCar,
                             IdService = sv.IdService,
                             NameService = sv.NameService,
                             Ngaytao = sv.Ngaytao.Value,
                             TongTien = sv.TongTien.Value,
                             Type = sv.Type.Value,
                             TenTaiXe = tx.TenTaiXe,
                             Ghichu = sv.Ghichu
                         }
                       ).OrderByDescending(m => m.IdService).ToList();
            return model;
        }

        public IEnumerable<VM_DotBaoDuong> ReportDotbaoduong(int Idcar)
        {
            var model = (from d in dbcontext.bdDotbaoduongs 
                         join c in dbcontext.bdCars
                         on d.IDCar equals c.IDCar
                         join tx in dbcontext.bdTaixes
                         on c.IdTaiXe equals tx.IdTaixe
                         join tc in dbcontext.bdTypeCars
                         on c.IdTypeCar equals tc.TypeCar
                         where (Idcar==0 || d.IDCar==Idcar)
                         select new VM_DotBaoDuong()
                         {
                             IdDotBaoDuong=d.IdDotBaoDuong,
                             IDCar=d.IDCar,
                             NgayBD=d.NgayBD,
                             NgayKT=d.NgayKT,
                             SokmDau=d.SokmDau,
                             SoKmCuoi=d.SoKmCuoi,
                             CarSignal=c.CarSignal,
                             TenTaiXe=tx.TenTaiXe,
                             SoKmHientai=d.SoKmHientai.Value,
                             DinhMucBaoDuong=tc.DinhMucBaoDuong.Value,
                             Sokmconlai= tc.DinhMucBaoDuong.Value- d.SoKmHientai.Value,
                             Chisodukienlansau = tc.DinhMucBaoDuong.Value+ d.SokmDau.Value
                         }).OrderByDescending(m=>m.IdDotBaoDuong);
            var mgb = model.GroupBy(m => m.IDCar).ToList();
            List<VM_DotBaoDuong> lstVMBaoduong = new List<VM_DotBaoDuong>();
            foreach(var item in mgb)
            {
                var lastitem = model.Where(m=>m.IDCar==item.Key).FirstOrDefault();
                lstVMBaoduong.Add(lastitem);
            }
            return lstVMBaoduong;
        }
        #endregion
    }
}
