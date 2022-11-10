using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using DAL.Models;
using ClosedXML.Excel;
using System.IO;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace GiaoNhan.Controllers
{
    public class ImportController : Controller
    {
        BAL_User account = new BAL_User();
        BAL_Tracking trackinng = new BAL_Tracking();
        BAL_Permission Permission = new BAL_Permission();
        public class GiaoNhan
        {
            public string ID { get; set; }
            public string Code { get; set; }
            public DateTime DateGiaoNhan { get; set; }
            public int Status { get; set; }
        }
        // GET: Import
        public ActionResult Index()
        {
            var model = account.GetAll();
            ViewBag.Permission = Permission.GetALl();
            return View(model);
        }

      
        [HttpPost]
        public JsonResult Import(string Filename)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Data/") + Filename;

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            List<GiaoNhan> lstGiaonhan = new List<GiaoNhan>();
            string strBuider = "";
            for (int i = 2; i <= rowCount; i++)
            {
                if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 2].Value != null)
                {
                    try
                    {
                        GiaoNhan GiaoNhan = new GiaoNhan();
                        GiaoNhan.ID = xlRange.Cells[i, 1].Value.ToString();
                        GiaoNhan.Code = (xlRange.Cells[i, 2].Value.ToString()); 
                        if(GiaoNhan.Code== "X221007024-L")
                        {
                            int test = 10;
                        }
                        if (GiaoNhan.Code.Substring(0,1) != "X")
                        {
                            GiaoNhan.Code = "X" + GiaoNhan.Code;
                        }
                        var data = xlRange.Cells[i, 3].Value.ToString();
                        GiaoNhan.DateGiaoNhan = DateTime.ParseExact(xlRange.Cells[i, 3].Value.ToString(),"dd/MM/yyyy hh:mm:ss", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                        if (CheckExistTracking(GiaoNhan.Code, GiaoNhan.DateGiaoNhan))
                        {
                            GiaoNhan.Status = 2;
                        }
                        else
                        {
                            GiaoNhan.Status =1;
                        }
                        lstGiaonhan.Add(GiaoNhan);
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }

            var model = lstGiaonhan;
            xlWorkbook.Close(0);
            xlApp.Quit();
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public bool AsynData(List<GiaoNhan> listAsyn, int UserID)
        {
            int i = 0;
            var lstfind = listAsyn.Where(m => m.Code == "X221007024-L").ToList();
            try
            {
                List<tbTracking> lstTracking = new List<tbTracking>();
                foreach (var item in listAsyn)
                {
                    if (item.Code == "X221007024-L")
                    {
                        int chk = 10;
                    }
                    if (!CheckExistTracking(item.Code, item.DateGiaoNhan))
                    {
                       
                        if (item.DateGiaoNhan.Minute != 0 || item.DateGiaoNhan.Second!=0)
                        {
                            tbTracking tbTracking = new tbTracking();
                            tbTracking.TrackingCode = item.Code;
                            tbTracking.TrackingDate = item.DateGiaoNhan;
                            tbTracking.UserID = UserID;
                            //Kiểm tra trùng 
                            if (!trackinng.IsExist(tbTracking))
                            {
                                trackinng.InsertData(tbTracking);
                            }
                            else
                            {
                                int trung = 10;
                            }
                        }

                        i++;
                    }
                    else
                    {
                        
                    }

                } 

                return true;
            }
            catch (Exception ex)
            {
                var a = i;
                return false;
            }
        }

        public System.Data.DataTable ToDataTable<T>(List<T> items)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        [HttpPost]
        public bool ExportData()
        {
            List<tbTracking> lstTracking = new List<tbTracking>();
            lstTracking.Add(new tbTracking { TrackingCode = "ACB" });
            lstTracking.Add(new tbTracking { TrackingCode = "BCS" });
            try
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    workbook.Worksheets.Add(ToDataTable(lstTracking));
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/Data/") + "abc.xlsx";
                    workbook.SaveAs(path);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CheckExistTracking(string TrackingCode, DateTime TrackingDate)
        {
            return trackinng.CheckExistTracking(TrackingCode, TrackingDate);
        }

        public void ExportToExcel(Microsoft.Office.Interop.Excel._Application app, Microsoft.Office.Interop.Excel._Workbook workbook, GridView gridview, string SheetName, int sheetid)
        {
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets["Sheet" + sheetid];

            // see the excel sheet behind the program
            app.Visible = true;

            // get the reference of first sheet. By default its name is Sheet1.
            // store its reference to worksheet

            // worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook,Worksheets.Add();

            // changing the name of active sheet
            worksheet.Name = SheetName;

            // storing header part in Excel
            for (int i = 1; i < gridview.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = gridview.Columns[i - 1].HeaderText;
            }



            // storing Each row and column value to excel sheet
            for (int i = 0; i < gridview.Rows.Count - 1; i++)
            {
                for (int j = 0; j < gridview.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = gridview.Rows[i].Cells[j].Text.ToString();
                }
            }
            //save the application

            workbook.SaveAs(@"C:\Users\test\Desktop\Test\" + "asd.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        }
    } 
}