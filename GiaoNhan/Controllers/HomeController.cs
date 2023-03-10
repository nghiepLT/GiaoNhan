using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Office.Interop;
using System.Data;
using System.Data.OleDb;
using BAL;
using DAL.Models;
using System.Net.Mail;
//using GiaoNhan.Models;
namespace GiaoNhan.Controllers
{
    public class HomeController : Controller
    {
        BAL_User user = new BAL_User();
        BAL_Permission permission = new BAL_Permission();
        BAL_Role balrole = new BAL_Role();
        //KpiManagerEntities dbcontext = new KpiManagerEntities();
        public ActionResult Index()
        {
            string returnMenu = "/";
            if (Request.Cookies["trakinglogin"] != null)
            {
                var userName = Request.Cookies["trakinglogin"].Value;
                tbUSer tbaccount = user.GetAccountID(userName);
                ViewBag.Menu = balrole.GetLeftMenu(tbaccount.UserID).OrderByDescending(m=>m.STT).FirstOrDefault().MenuURL;
                returnMenu = ViewBag.Menu;
                return Redirect(returnMenu);
            }
            return Redirect("/login");
        }
      
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SlideBarMenu()
        {
            if (Request.Cookies["trakinglogin"] != null)
            {
                var userName = Request.Cookies["trakinglogin"].Value;
                tbUSer tbaccount = user.GetAccountID(userName);
                ViewBag.ListMN = balrole.GetLeftMenu(tbaccount.UserID);
               // ViewBag.ListMenu = permission.GetMenuByPermission(tbaccount.PermissionID);
                ViewBag.AccountUser = tbaccount.UserName;
            }
            return PartialView();
        }
        public ActionResult Rightnavbar()
        {
            if (Request.Cookies["trakinglogin"] != null)
            { 
                var userName = Request.Cookies["trakinglogin"].Value;
                tbUSer tbaccount = user.GetAccountID(userName);
                ViewBag.AccountUser = tbaccount.UserName;
                //Gửi mail Kiểm định Xe
                var chkEmail=  user.CheckSendmailKiemDinh();
                string title = "";
                string content = "";
                title = "Cảnh báo kiểm định xe";
                if (chkEmail == false)
                {
                    var lstcar = user.GetListCarEmail();
                    foreach(var item in lstcar)
                    {
                        content = "";
                        content += "<table>";

                        // Row1
                        content += "<tr style=\"padding:2px 10px\">";
                        content += "<td>";
                        content += "Dear Anh Minh,";
                        content += "</td>";
                        content += "</tr>";

                        // Row2
                        content += "<tr style=\"padding:2px 10px\">";
                        content += "<td>";
                        content += "Xe " + item.CarSignal +  " đã gần tới thời gian kiểm định vào ngày "+item.ThoiGianDangKiem.Value.ToShortDateString();
                        content += "</td>";
                        content += "</tr>";

                        content += "</table>";
                       // sendEmail("minhtn@nguyenkimvn.vn", title, content);
                    }
                 //   user.TaoEmailKiemDinh();
                }
            }
            return PartialView();
        }
        public ActionResult Loading()
        {
            return PartialView();
        }
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