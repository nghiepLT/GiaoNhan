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
    public class BAL_Service
    {
        DAL_Service dalService = new DAL_Service();
        public IEnumerable<VM_Service> GetListTypeCar()
        {
            return dalService.GetListService();
        }
        public bdService GetServiceByID(int IdService)
        {
            return dalService.GetServiceByID(IdService);
        }
        public bool InsertService(bdService bdService)
        {
            return dalService.InsertService(bdService);
        }
        public bool DeleteService(int IdService)
        {
            return dalService.DeleteService(IdService);
        }
    }
}
