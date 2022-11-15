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
    public class BAL_Role
    {
        DAL_Role dalRole = new DAL_Role();
        public IEnumerable<VM_Role> GetAllRole()
        {
            return dalRole.GetAllRole();
        }
        public bool InsertRole(tbRole role, List<int> lstMenu)
        {
            return dalRole.InsertRole(role, lstMenu);
        }
        public VM_Role GetRole(int RoleId)
        {
            return dalRole.GetRole(RoleId);
        }
        public bool DeleteRole(int RoleId)
        {
            return dalRole.DeleteRole(RoleId);
        }
        public IEnumerable<tbMenu> GetLeftMenu(int UserID)
        {
            return dalRole.GetLeftMenu(UserID);
        }
    }
}
