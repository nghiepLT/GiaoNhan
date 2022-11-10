using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
namespace BAL
{
    public class BAL_Config
    {
        DAL_Config config = new DAL_Config();
        public tbConfig Getconfig()
        {
            return config.Getconfig();
        }
        public bool UpdateConfig(tbConfig tbConfig)
        {
            return config.UpdateConfig(tbConfig);
        }
    }
}
