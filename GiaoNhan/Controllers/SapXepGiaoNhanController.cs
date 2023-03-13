﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using DAL.Models;
namespace GiaoNhan.Controllers
{
    public class SapXepGiaoNhanController : Controller
    {
        BAL_SapXep balSapxep = new BAL_SapXep();
        // GET: SapXepGiaoNhan
        public ActionResult SapXep()
        {
            ViewBag.ListGiaonhan = balSapxep.GetListDSGiaoNhan().ToList();
            var getData = balSapxep.GetLastSapXepconfig();
            if (getData != null)
                ViewBag.Data = balSapxep.GetLastPosition();
            ViewBag.ListHistory = balSapxep.GetListSapXepDetail();

            return View();
        }

        public bool SapxepInsert(string array)
        {
            var getsplt = array.Split(',');
            tbSapXepConfig tbspconfig = new tbSapXepConfig();
            tbspconfig.Position = array;
            tbspconfig.NgayCapNhat = DateTime.Now;
            return balSapxep.SapXepInsert(tbspconfig);
        }

        public bool KiemTraTheHoatDong(string MaThe)
        {
            return balSapxep.KiemTraTheHoatDong(MaThe);
        }

       public bool SwapCode(string FirstCode,string TwoCode)
        {
            return balSapxep.SwapCode(FirstCode, TwoCode);
        }
    }
}