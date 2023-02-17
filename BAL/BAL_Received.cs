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

    public class BAL_Received
    {
        DAL_Received received = new DAL_Received();
        public IEnumerable<tbReceived> GetAll()
        {
            return received.GetAll();
        }
        public IEnumerable<VM_Received> GetAllByIDUser(int UserID)
        {
            return received.GetAllByIDUser(UserID);
        }
        public tbReceived GetById(int ReceivedID)
        {
            return received.GetById(ReceivedID);
        }
        public bool Insert(tbReceived tbReceived,int UserID)
        {
            try
            {
                received.Insert(tbReceived, UserID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool Update(int ReceivedID,int NCCID,string Products, int SLNhap, int SlKiemTra,int type)
        {
            return received.Update(ReceivedID,NCCID, Products, SLNhap, SlKiemTra, type);
        }
        public IEnumerable<tbNCC> GetNCC()
        {
            return received.GetNCC();
        }
        public IEnumerable<tbProduct> GetProduct()
        {
            return received.GetProduct();
        }
        public bool DeleteRecived(int ReceivedID)
        {
            return received.DeleteRecived(ReceivedID);
        }
        public VM_Received GetPopupInfo(int ReceivedID)
        {
            return received.GetPopupInfo(ReceivedID);
        }
        public IEnumerable<tbUSer> GetNhanVienTrungChuyen()
        {
            return received.GetNhanVienTrungChuyen();
        }
        public IEnumerable<tbUSer> GetNhanVienXepHang()
        {
            return received.GetNhanVienXepHang();
        }
        public void ChuyenPhieu(int ReceivedID, string UserID,int Time)
        {
              received.ChuyenPhieu(ReceivedID, UserID, Time);
        }
        public bool KetThucDonChuyen(int ReceivedID)
        {
            return received.KetThucDonChuyen(ReceivedID); 
        }
    }
}
