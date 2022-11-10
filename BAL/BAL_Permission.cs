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
    public class BAL_Permission
    {
        DAL_Permission permission = new DAL_Permission();
        public IEnumerable<VM_Permission_Menu> GetMenuByPermission(int PermissionID)
        {
            return permission.GetMenuByPermission(PermissionID);
        }
        public tbPermission GetByID(int PermissionID)
        {
            return permission.GetByID(PermissionID);
        }
        public IEnumerable<tbPermission> GetALl()
        {
            return permission.GetALl();
        }
        public IEnumerable<tbMenu> GetALlMenu()
        {
            return permission.GetALlMenu();
        }
    }
}
