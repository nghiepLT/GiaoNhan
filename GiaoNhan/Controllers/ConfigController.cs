using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using DAL.Models;
using Newtonsoft.Json;

namespace GiaoNhan.Controllers
{
    public class ConfigController : Controller
    {

        BAL_Config balConfig = new BAL_Config();
        // GET: Config
        public ActionResult Index()
        {
            var model = balConfig.Getconfig();
            return View(model);
        }

        [HttpPost]
        public bool UpdateConfig(string data)
        {
            tbConfig _tbconfig = JsonConvert.DeserializeObject<tbConfig>(data);
            return balConfig.UpdateConfig(_tbconfig);
        }
            
    }
}