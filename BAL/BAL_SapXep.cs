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
    public class BAL_SapXep
    {
        DAL_SapXep dalSapxep = new DAL_SapXep();
        public IEnumerable<VM_UserSapXep> GetListDSGiaoNhan()
        {
            return dalSapxep.GetListDSGiaoNhan();
        }
        public bool SapXepInsert(tbSapXepConfig tbconfig)
        {
            return dalSapxep.SapXepInsert(tbconfig);
        }
        public tbSapXepConfig GetLastSapXepconfig()
        {
            return dalSapxep.GetLastSapXepconfig();
        }
        public IEnumerable<VM_SapXepUser> GetListUSer(string position)
        {
            return dalSapxep.GetListUSer(position);
        }
        public bool Checkdone(string Mathe)
        {
            return dalSapxep.Checkdone(Mathe);
        }
        public bool CreateSapXepDetail()
        {
            return dalSapxep.CreateSapXepDetail();
        }
        public tbSapXepDetail GetSapXepDetail()
        {
            return dalSapxep.GetSapXepDetail();
        }
        public bool CapNhatSapXepDetail(int ThetaiID, int Status)
        {
            return dalSapxep.CapNhatSapXepDetail(ThetaiID, Status);
        }
        public tbUSer GetUserIdByCode(string Code)
        {
            return dalSapxep.GetUserIdByCode(Code);
        }
        public IEnumerable<tbSapXepDetail> GetListSapXepDetail()
        {
            return dalSapxep.GetListSapXepDetail();
        }
        public bool KiemTraTheHoatDong(string Mathe)
        {
            return dalSapxep.KiemTraTheHoatDong(Mathe);
        }
        public bool SwapCode(string FirstCode, string TwoCode)
        {
            return dalSapxep.SwapCode(FirstCode, TwoCode);
        }
        public string GetLastPosition()
        {
            return dalSapxep.GetLastPosition();
        }
    }
}
