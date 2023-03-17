using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.ViewModels;

namespace BAL
{
    public class BAL_TheTai
    {
        DAL_TheTai dalTheTai = new DAL_TheTai();
        public tbTheTai CheckExist(string MaThe,DateTime now)
        {
            return dalTheTai.CheckExist(MaThe,now);
        }
        public bool InsertData(tbTheTai tbTheTai)
        {
            return dalTheTai.InsertData(tbTheTai);
        }
        public bool UpdateData(tbTheTai tbTheTai)
        {
            return dalTheTai.UpdateData(tbTheTai);
        }
        public tbTheTai GetLastTheTai(string MaThe)
        {
            return dalTheTai.GetLastTheTai(MaThe);
        }
        public bool UpdatePhieuXuat(int ThetaiID, string MaPhieu)
        {
            return dalTheTai.UpdatePhieuXuat(ThetaiID, MaPhieu);
        }
        public IEnumerable<VM_TheTai> LoadTracking(int UserID)
        {
            return dalTheTai.LoadTracking(UserID);
        }
        public bool UpdateDataDetail(tbTheTaiChiTiet tbThetaichitiet)
        {
            return dalTheTai.UpdateDataDetail(tbThetaichitiet);
        }
        public IEnumerable<tbTheTaiChiTiet> GetAlltheTaiChiTiet(string MaThe)
        {
            return dalTheTai.GetAlltheTaiChiTiet(MaThe);
        }
        public bool UpdateStatus(int TheTaiChiTietID)
        {
            return dalTheTai.UpdateStatus(TheTaiChiTietID);
        }
        public string GetGroupTheTai()
        {
            return dalTheTai.GetGroupTheTai();
        }
        public string GetGroupTheTaiwait() //n2fix
        {
            return dalTheTai.GetGroupTheTaiwait();
        }
        public bool CheckDetailThetai(string MaPhieu)
        {
            return dalTheTai.CheckDetailThetai(MaPhieu);
        }
        public bool CheckMaHopLe(string MaThe)
        {
            return dalTheTai.CheckMaHopLe(MaThe);
        }
        public bool UpdateCancle(int TheTaiChiTietID, string Description,int Type, int? TienPhatSinh, int? SoKMPhatSinh,int? NhanTienMat)
        {
            return dalTheTai.UpdateCancle(TheTaiChiTietID, Description, Type, TienPhatSinh, SoKMPhatSinh, NhanTienMat);
        }
        public bool CapnhatLuotDi(int ThetaiID)
        {
            return dalTheTai.CapnhatLuotDi(ThetaiID);
        }
        public bool CapnhatLuotVe(string  Code)
        {
            return dalTheTai.CapnhatLuotVe(Code);
        }
        public bool KiemTraGiaoHetPhieu(string Code)
        {
            return dalTheTai.KiemTraGiaoHetPhieu(Code);
        }
        public bool KiemTraKetThucLuot(string MaThe)
        {
            return dalTheTai.KiemTraKetThucLuot(MaThe);
        }
       
    }
}
