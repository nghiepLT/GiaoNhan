using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL;
using DAL.ViewModels;
namespace BAL
{
    public class BAL_Tracking
    {
        DAL_Tracking tracking = new DAL_Tracking();
        public bool InsertData(tbTracking tbTracking)
        {
            return tracking.InsertData(tbTracking);
        }
        public bool UpdateData(tbTracking tbTracking)
        {
            return tracking.UpdateData(tbTracking);
        }
        public IEnumerable<VM_Tracking> LoadTracking(int UserID)
        {
            return tracking.LoadTracking(UserID);
        }
        public IEnumerable<tbTracking> LoadReportTracking(int UserID, DateTime fromDate, DateTime toDate)
        {
            return tracking.LoadReportTracking(UserID, fromDate, toDate);
        } 
        public bool IsExist(tbTracking tbTracking)
        {
            return tracking.IsExist(tbTracking);
        }
        public IEnumerable<tbDriver> GetAllDriver()
        {
            return tracking.GetAllDriver();
        }
        public IEnumerable<VM_Received> LoadReceivedData(int UserID, DateTime fromDate, DateTime toDate)
        {
            return tracking.LoadReceivedData(UserID, fromDate, toDate);
        }
        public IEnumerable<VM_TrackingList> LoadTrackingDataList(int UserID, DateTime fromDate, DateTime toDate,string Code)
        {
            return tracking.LoadTrackingDataList(UserID, fromDate, toDate, Code);
        }
        public IEnumerable<VM_ReceivedList> LoadReceivedDataList(int UserID, DateTime fromDate, DateTime toDate)
        {
            return tracking.LoadRecivedDataList(UserID, fromDate, toDate);
        }
        public bool CheckExistTracking(string TrackingCode, DateTime TrackingDate)
        {
            return tracking.CheckExistTracking(TrackingCode, TrackingDate);
        }
        public tbTracking CheckExist(string TrackingCode,int accountID)
        {
            return tracking.CheckExist(TrackingCode, accountID);
        }
        public tbTracking CheckExistOnlyCode(string TrackingCode)
        {
            return tracking.CheckExistOnlyCode(TrackingCode);
        }
        public int GetKPI(DateTime ? dateStart, DateTime? dateEnd, int? Totals,string type)
        {
            return tracking.GetKPI(dateStart, dateEnd, Totals, type);
        }
        public bool CheckCodeDelivery(string Code)
        {
            return tracking.CheckCodeDelivery(Code);
        }
        public bool CheckedCode(int UserID, string TrackingCode)
        {
            return tracking.CheckedCode(UserID, TrackingCode);
        }
        public int CheckedNumber(string TrackingCode)
        {
            return tracking.CheckedNumber(TrackingCode);
        }
        public bool UpdateCountStep(int TrackingID, int CountStep)
        {
            return tracking.UpdateCountStep(TrackingID, CountStep);
        }
        public Dictionary<int,int> CheckSoLuong(int TrackingID, string TrackingCode)
        {
            return tracking.CheckSoLuong(TrackingID, TrackingCode);
        }
        public int GetSLRemain(string TrackingCode)
        {
            return tracking.GetSLRemain(TrackingCode);
        }
    }
}
 