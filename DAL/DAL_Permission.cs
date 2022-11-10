using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.ViewModels;
namespace DAL
{
    public  class DAL_Permission
    {
        KpiManagerEntities dbcontext = new KpiManagerEntities();
        public IEnumerable<VM_Permission_Menu> GetMenuByPermission(int PermissionID)
        {
            var model = (from pm in dbcontext.tbPermission_Menu
                         join mn in dbcontext.tbMenus
                         on pm.MenuID equals mn.MenuId
                         where (pm.PermissionID == PermissionID) || PermissionID==0
                         select new VM_Permission_Menu()
                         {
                             ID=pm.ID,
                             MenuID=mn.MenuId,
                             PermissionID=pm.PermissionID,
                             MenuName=mn.MenuName,
                             MenuURL=mn.MenuURL,
                             Icon=mn.Icon,
                             STT=pm.STT

                         }
                       ).OrderBy(m=>m.STT); 
            return model;
        }
        public tbPermission GetByID(int PermissionID)
        {
            return dbcontext.tbPermissions.Find(PermissionID);
        }
        public IEnumerable<tbPermission> GetALl()
        {
            return dbcontext.tbPermissions.Where(m=>m.PermissionID!=1);
        }
        public IEnumerable<tbMenu> GetALlMenu()
        {
            return dbcontext.tbMenus;
        }
        
    }
}
