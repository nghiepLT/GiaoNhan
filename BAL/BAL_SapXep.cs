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
    public class BAL_SapXep
    {
        DAL_SapXep dalSapxep = new DAL_SapXep();
        public IEnumerable<tbUSer> GetListDSGiaoNhan()
        {
            return dalSapxep.GetListDSGiaoNhan();
        }
        public bool SapXepInsert(tbSapXepConfig tbconfig)
        {
            return dalSapxep.SapXepInsert(tbconfig);
        }
        public tbSapXepConfig GetLastSapXepconfig()
        {
            return dalSapxep.GetLastSapXepconfig();
        }
        public IEnumerable<VM_SapXepUser> GetListUSer(string position)
        {
            return dalSapxep.GetListUSer(position);
        }
        public bool Checkdone(string Mathe)
        {
            return dalSapxep.Checkdone(Mathe);
        }
    }
}
