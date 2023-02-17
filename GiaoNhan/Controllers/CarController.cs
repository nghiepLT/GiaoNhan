using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using Newtonsoft.Json;
using DAL.Models;
using System.Net.Mail;

namespace GiaoNhan.Controllers
{
    public class CarController : Controller
    {
        BAL_Car balCar = new BAL_Car();
        #region Typecar
        public ActionResult TypeCarIndex()
        {
            
            var model = balCar.GetListTypeCar();
            return View(model);
        }
        public ActionResult GetListTypeCar()
        {
            var model = balCar.GetListTypeCar();
            return PartialView(model);
        }
        public bool InsertTypeCar(string data)
        {
            try
            {
                bdTypeCar vm = JsonConvert.DeserializeObject<bdTypeCar>(data);
                balCar.InsertTypeCar(vm);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public JsonResult GetTypeCarByID(int TypeCar)
        {
            var model = balCar.GetTypeCarByID(TypeCar);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public bool DeleteTypeCar(int TypeCar)
        {
            return balCar.DeleteTypeCar(TypeCar);
        }
        #endregion
        #region Car
        // GET: Car
        public ActionResult CarIndex()
        {
            ViewBag.ListTypeCar = balCar.GetListTypeCar();
            ViewBag.ListTaiXe = balCar.GetListTaiXe();
            var model = balCar.GetListTypeCar();
            return View(model);
        }
        public ActionResult CarChildIndex(int TypeCar)
        {
            var model = balCar.GetListCarByID(TypeCar);
            return PartialView(model);
        }
        public bool InsertCar(string data)
        {
            try
            {
                bdCar vm = JsonConvert.DeserializeObject<bdCar>(data);
                balCar.InsertCar(vm);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteCar(int IDCar)
        {
            return balCar.DeleteCar(IDCar);
        }
        public JsonResult GetCarByID(int IDCar)
        {
            var model = balCar.GetCarByID(IDCar);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Tai xế
        public ActionResult TaixeIndex()
        { 
            var model = balCar.GetListTaiXe();
            return View(model);
        }
        public bool InsertTaiXe(string data)
        {
            try
            {
                bdTaixe vm = JsonConvert.DeserializeObject<bdTaixe>(data);
                balCar.InsertTaiXe(vm);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public JsonResult GetTypeTaiXerByID(int IdTaixe)
        {
            var model = balCar.GetTaiXeByID(IdTaixe);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public bool DeleteTaiXe(int IdTaixe)
        {
            return balCar.DeleteTaiXe(IdTaixe);
        }
        #endregion

        #region Hành trình xe
        public ActionResult HanhTrinhXe()
        {
            ViewBag.Listcar = balCar.GetListCar();
            return View();
        }

        public bool InsertDotBaoDuong(int IDCar,DateTime NgayBD,int SokmDau)
        {
            try
            {
                balCar.InsertDotBaoDuong(IDCar, NgayBD, SokmDau);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public ActionResult GetDotBaoDuong(int IDCar)
        {
            var model = balCar.GetDotBaoDuong(IDCar);
            ViewBag.Listchild= balCar.GetHanhtrinhbaotri(IDCar);
            return PartialView(model);
        }
        public ActionResult GetHanhTrinh(int IdDotBaoDuong)
        {
            var model = balCar.GetHanhtrinhbaotri(IdDotBaoDuong);
            return PartialView(model);
        }
        public bool CapnhatsoKM(int IDCar, int SoKM)
        {
            if (balCar.CheckSendEmail(IDCar, SoKM))
            {
                string title = "";
                string content = "";
                title = "Cảnh báo bảo dưỡng";
                bdCar bdCar = balCar.GetCarByID(IDCar);
               var dotbaoduong= balCar.GetDotBaoDuong(IDCar);

                content+= "<table>";

                // Row1
                content += "<tr style=\"padding:2px 10px\">";
                content += "<td>";
                content += "Dear Anh Minh,";
                content += "</td>";
                content += "</tr>";

                // Row2
                content += "<tr style=\"padding:2px 10px\">";
                content += "<td>";
                content += "Xe "+ bdCar.CarSignal+"-"+ dotbaoduong.TenTaiXe+  " đã chạy gần tới định mức bảo dưỡng với số Km hiện tại là "+ dotbaoduong.SoKmHientai;
                content += "</td>";
                content += "</tr>";

                content += "</table>";
               // sendEmail("minhtn@nguyenkimvn.vn", title,content);
            }
            return balCar.CapnhatsoKM(IDCar, SoKM);
        }
        public bool DeleteHanhtrinh(int IDHanhTrinhBaoTri)
        {
            return balCar.DeleteHanhtrinh(IDHanhTrinhBaoTri);
        }
        #endregion

        private bool sendEmail(string to, string title, string sContent)
        {
            try
            {

                string AdminEmail = "nghiphep@nguyenkimvn.vn";
                string AdminPass = "12345678";
                string MailHost = "192.168.117.200";
                int intPort = 25;
                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Credentials = new System.Net.NetworkCredential(AdminEmail, AdminPass);
                SmtpServer.Port = intPort;
                SmtpServer.Host = MailHost;
                if (intPort == 25)
                    SmtpServer.EnableSsl = false;
                else
                    SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.From = new MailAddress(AdminEmail, Request.Url.Host.ToString(), System.Text.Encoding.UTF8);
                mail.To.Add(to);
                //cc

                mail.Subject = title;
                mail.Body = sContent;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.IsBodyHtml = true;
                SmtpServer.Send(mail);
                return true;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}