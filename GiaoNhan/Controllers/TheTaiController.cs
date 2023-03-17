using BAL;
using DAL;
using DAL.Models;
using DAL.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GiaoNhan.Controllers
{
    public class TheTaiController : Controller
    {
        BAL_Tracking tracking = new BAL_Tracking();
        BAL_User user = new BAL_User();
        BAL_Permission Permission = new BAL_Permission();
        BAL_Config balConfig = new BAL_Config();
        BAL_TheTai balTheTai = new BAL_TheTai();
        BAL_SapXep balSapxep = new BAL_SapXep();
        // GET: TheTai
        public ActionResult QuetTheTai()
        {
            ViewBag.ListTheTai = balTheTai.GetGroupTheTai();
            ViewBag.ListTheTai2 = balTheTai.GetGroupTheTaiwait();
            balSapxep.CreateSapXepDetail();
            return View();
        }
        public ActionResult LoadSapXep()
        {
            //var getData = balSapxep.GetLastSapXepconfig();
            //if (getData != null)
            //    ViewBag.ListUSer = balSapxep.GetListUSer(balSapxep.GetLastSapXepconfig().Position);
            var getData = balSapxep.GetSapXepDetail();
            if (getData != null)
                ViewBag.ListUser = balSapxep.GetListUSer(getData.Position);
            return PartialView();
        }
        public ActionResult LoadSapXepNew()
        { 
            var getData = balSapxep.GetLastSapXepTai();
            if (getData != null)
            {
                ViewBag.ListUser = balSapxep.GetListUSer(getData.PositionEmpty);
                ViewBag.ListUserDone = balSapxep.GetListUSer(getData.PositionDone);
            }
            return PartialView();
        }

        public VM_Json GetJsonData(string spx)
        {
            try
            {
                string firstUrl = balConfig.Getconfig().ApiUrl;
                var url = firstUrl + "jstotalpx.asp?SPX=" + spx;
                var textFromFile = (new WebClient()).DownloadString(url);
                VM_Json vm = JsonConvert.DeserializeObject<VM_Json>(textFromFile);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public ActionResult LoadTheTai()
        {
            var userName = Request.Cookies["trakinglogin"].Value;
            if (userName.ToLower() == "TheTai".ToLower() || userName.ToLower()== "minhtn".ToLower())
                ViewBag.Type = 1;
            else
                ViewBag.Type = 2;
            int accountID = user.GetAccountID(userName).UserID;
            var model = balTheTai.LoadTracking(accountID);
            return PartialView(model);
        }
        public bool CapNhatLuotDi(int ThetaiID)
        {
            return balTheTai.CapnhatLuotDi(ThetaiID);
        }
        public bool CapNhatLuotVe()
        {
            var userName = Request.Cookies["trakinglogin"].Value;
            ViewBag.accountID = user.GetAccountID(userName).Code;
            return balTheTai.CapnhatLuotVe(ViewBag.accountID);
        }
        public int GetKPI(DateTime? dateStart, DateTime? dateEnd, int? Totals)
        {
            int kpiResult = 0;
            DAL_Config dalConfig = new DAL_Config();
            var config = dalConfig.Getconfig();
            if (dateEnd != null)
            {
                if (dateEnd.Value != DateTime.MinValue)
                {
                    var minusDateSS = Math.Round((dateEnd.Value - dateStart).Value.TotalSeconds);
                    int? NTG40 = 0;
                    //Nhập
                    NTG40 =Totals* int.Parse(config.TimeDelivery.ToString());

                    int TGKPI = (NTG40).Value;
                    kpiResult = int.Parse(minusDateSS.ToString()) - TGKPI;
                }

            }
            return kpiResult;
        }

        //1 Thành công
        //2 Chưa quét phiếu xuất
        //3 phiếu ko hợp lệ
        //4 Lỗi
        //5 Phiếu đã quét
        // 6 Phiếu đã kết thúc
        // 7 Không có trong danh sách sắp xếp
        // 8 Chưa tới lượt quét
        // 9 Chưa kết thúc lượt về 
        //11 Lượt chưa kết thúc, không thể quét thêm
        public int Insert(string data,string MaTheSL)
        {
            if (Request.Cookies["trakinglogin"] != null)
            {
               
                var userName = Request.Cookies["trakinglogin"].Value;
                int accountID = user.GetAccountID(userName).UserID;
                tbTheTai tbtheTai = JsonConvert.DeserializeObject<tbTheTai>(data);
                tbtheTai.MaThe = tbtheTai.MaThe.ToUpper();
                string type = "";
                var getPermission = user.GetbyID(accountID);

                //Kiểm tra
                if (!balTheTai.CheckMaHopLe(tbtheTai.MaThe))
                {
                    return 3;
                }
                if (balTheTai.CheckDetailThetai(tbtheTai.MaThe))
                {
                    return 5;
                }
                //if (balTheTai.KiemTraKetThucLuot(tbtheTai.MaThe))
                //{
                //    return 11;
                //}
                //Kiểm tra theo lượt
                var getData = balSapxep.GetLastSapXepTai();
                var lstUser = balSapxep.GetListUSer(getData.PositionEmpty);
                if (tbtheTai.MaThe.Contains("GN"))
                {
                    foreach (var item in lstUser)
                    {
                        var checkInList = lstUser.Where(m => m.Code == tbtheTai.MaThe).FirstOrDefault();
                        if (checkInList == null)
                            return 7;
                        //if (checkInList.Status == 1)
                        //    return 9;
                        ////Kiểm tra lượt trước đó
                        //var indexof = lstUser.ToList().IndexOf(checkInList);
                        //if (indexof > 0)
                        //{
                        //    // //Kiểm tra lượt trước đó n-1
                        //    int indexprev = indexof - 1;
                        //    var current = lstUser.ToList()[indexprev];
                        //    if (current.Status == 0)
                        //        return 8;
                        //}
                    }
                }

                if (getPermission != null)
                {
                    int getPermissionID = getPermission.PermissionID;
                    var getNamePermission = Permission.GetByID(getPermissionID).PermissionName;

                    if (getNamePermission == "Xuất kho" || getNamePermission == "Kế toán")
                    {
                        return 3;
                    }
                }

                //Trường hợp quét thẻ tài
                if (tbtheTai.MaThe.Contains("GN"))
                {
                    tbTheTai ckhTracking = balTheTai.CheckExist(tbtheTai.MaThe,DateTime.Now.Date);

                    if (ckhTracking == null || (ckhTracking!=null && ckhTracking.DateEnd!=null))
                    {

                        tbtheTai.DateStart = DateTime.Now;
                        tbtheTai.DateCreate = DateTime.Now;
                        tbtheTai.UserId = accountID;
                       
                        return balTheTai.InsertData(tbtheTai)==true?1:4;
                    }
                    else
                    {
                        //if (tbtheTai.TrackingCode.Contains("X") && ckhTracking.DateEnd != null)
                        //    return false;
                        if (ckhTracking.MaPhieu == null)
                            return 2;
                            ckhTracking.DateEnd = DateTime.Now;
                        ckhTracking.KPI = tracking.GetKPI(ckhTracking.DateStart, ckhTracking.DateEnd, ckhTracking.Count, type);
                        balTheTai.UpdateData(ckhTracking);

                        //Cập nhật Sắp xếp Detail
                        var getuserid = balSapxep.GetUserIdByCode(tbtheTai.MaThe);
                        balSapxep.CapNhatSapXepDetail(getuserid.UserID, 1);
                        return 10; //n2fix 1=>10
                    }
                }
                //Trường hợp quét phiếu xuất
                else
                {
                    //Trường hợp nếu như chưa quét phiếu xuất
                    //Kiểm tra dòng mới nhất
                    tbTheTai lastThetai = balTheTai.GetLastTheTai(MaTheSL);
                    if (lastThetai!=null && lastThetai.DateEnd != null)
                        return 6;
                    if (lastThetai != null)
                    {
                        var getJson = GetJsonData(tbtheTai.MaThe);
                        lastThetai.MaPhieu +=tbtheTai.MaThe+ "|"+ getJson.ToTal+ ",";
                        lastThetai.Count = (lastThetai.Count != null ? lastThetai.Count : 0) + getJson.ToTal;
                        balTheTai.UpdateData(lastThetai);
                        //UPdate chi  tiết
                        tbTheTaiChiTiet ttct = new tbTheTaiChiTiet();
                        ttct.MaPhieu = tbtheTai.MaThe;
                        ttct.ThetaiID = lastThetai.ThetaiID;
                        ttct.Status = 0;
                        ttct.DateCreate = DateTime.Now;
                        ttct.MaThe = lastThetai.MaThe;
                        ttct.SOKM = getJson.SOKM;
                        ttct.SOKM = Math.Round(ttct.SOKM.Value,2);
                        balTheTai.UpdateDataDetail(ttct);
                        return 1;
                    }
                }
                
            }
            return 4;
        }


        public ActionResult KetThucGiaoNhan()
        {
            var userName = Request.Cookies["trakinglogin"].Value; 
            ViewBag.accountID = user.GetAccountID(userName).Code;
            var model = user.GetAll().Where(m => m.Code!=null && m.Code.Contains("GN")  && m.UserID!= 1019);
            ViewBag.Check = balTheTai.KiemTraGiaoHetPhieu(ViewBag.accountID);
            return View(model);
        }

        public ActionResult LoadKetThuc(string MaThe)
        {
            var model = balTheTai.GetAlltheTaiChiTiet(MaThe);
            return PartialView(model);
        }
        public static int testc()
        {
            return 1;
        }
        [HttpPost]
        public bool updateStatus(int TheTaiChiTietID)
        {
            return balTheTai.UpdateStatus(TheTaiChiTietID);
        }

        public bool UpdateCancle(int TheTaiChiTietID,string Description,int Type,int ? TienPhatSinh,int ? SoKMPhatSinh,int ? NhanTienMat)
        {
            if (TienPhatSinh == null)
                TienPhatSinh = 0;
            if (SoKMPhatSinh == null)
                SoKMPhatSinh = 0;
            return balTheTai.UpdateCancle(TheTaiChiTietID, Description, Type, TienPhatSinh, SoKMPhatSinh, NhanTienMat);
        }
    }
}