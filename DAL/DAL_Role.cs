using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.ViewModels;
using DAL;
using DAL.Models;
namespace DAL
{
    public class DAL_Role
    {
        KpiManagerEntities dbcontext = new KpiManagerEntities();
        public IEnumerable<VM_Role> GetAllRole()
        {
            List<VM_Role> lstVMRole = new List<VM_Role>();
            var grby = dbcontext.tbRoleMenus.GroupBy(m => m.RoleId);
            foreach(var gr in grby)
            {
                var currentRole = dbcontext.tbRoles.Find(gr.Key.Value);
                VM_Role _VM_Role = new VM_Role();
                _VM_Role.RoleId = currentRole.RoleId;
                _VM_Role.RoleName = currentRole.RoleName;
                var lstChild = dbcontext.tbRoleMenus.Where(m => m.RoleId == currentRole.RoleId);
                string strMenu = "";
                var lstMenu = new List<tbMenu>();
                foreach(var item in lstChild)
                {
                    tbMenu mn = dbcontext.tbMenus.Find(item.MenuId);
                    lstMenu.Add(mn);
                }
                _VM_Role.lsttbMenu = lstMenu;
                lstVMRole.Add(_VM_Role);
            }
            return lstVMRole;
        }
        public bool InsertRole(tbRole role,List<int> lstMenu)
        {
            if (role.RoleId == 0)
            {
                tbRole _tbRole = new tbRole();
                _tbRole.RoleName = role.RoleName;
                dbcontext.tbRoles.Add(_tbRole);
                dbcontext.SaveChanges();
                foreach(var item in lstMenu)
                {
                    tbRoleMenu tbRoleMenu = new tbRoleMenu();
                    tbRoleMenu.MenuId = item;
                    tbRoleMenu.RoleId = _tbRole.RoleId;
                    dbcontext.tbRoleMenus.Add(tbRoleMenu);
                    dbcontext.SaveChanges();
                }
                //Insert Menu
                return true;
            }
            else
            { 
                tbRole _tbRole = dbcontext.tbRoles.Find(role.RoleId);
                _tbRole.RoleName = role.RoleName;
                dbcontext.SaveChanges();
                //Edit menu
                //List insert
                var lstInsert = lstMenu.Where(m => !dbcontext.tbRoleMenus.Where(j=>j.RoleId==role.RoleId).Any(n => n.MenuId == m)).ToList();
                foreach(var item in lstInsert)
                {
                    tbRoleMenu tbRoleMenu = new tbRoleMenu();
                    tbRoleMenu.MenuId = item;
                    tbRoleMenu.RoleId = _tbRole.RoleId;
                    dbcontext.tbRoleMenus.Add(tbRoleMenu);
                    try
                    {
                        dbcontext.SaveChanges();

                    }
                    catch(Exception ex)
                    {

                    }
                }
                var lstDelete = dbcontext.tbRoleMenus.Where(j => j.RoleId == role.RoleId).Where(m => !lstMenu.Any(n => n == m.MenuId)).ToList();
                foreach (var item in lstDelete)
                {
                    tbRoleMenu tbRoleMenu = dbcontext.tbRoleMenus.Where(m=>m.RoleId==_tbRole.RoleId && m.MenuId==item.MenuId).FirstOrDefault();
                    if (tbRoleMenu != null)
                    {
                        dbcontext.tbRoleMenus.Remove(tbRoleMenu);
                        dbcontext.SaveChanges();
                    }

                }
            }
            return false;
        }

        public VM_Role GetRole(int RoleId)
        {
            var model = dbcontext.tbRoleMenus.Where(m => m.RoleId == RoleId);
            var cuurentRole = dbcontext.tbRoles.Where(m => m.RoleId == RoleId).FirstOrDefault();
            VM_Role _VM_Role = new VM_Role();
            _VM_Role.RoleId = cuurentRole.RoleId;
            _VM_Role.RoleName = cuurentRole.RoleName; 
            foreach(var item in model)
            {
                tbMenu mn = dbcontext.tbMenus.Find(item.MenuId);
                _VM_Role.lsttbMenu.Add(mn);
            } 

            return _VM_Role;
        }
        public bool DeleteRole(int RoleId)
        {
            try
            {
                var model = dbcontext.tbRoleMenus.Where(m => m.RoleId == RoleId).ToList();
                dbcontext.tbRoleMenus.RemoveRange(model);
                dbcontext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<tbMenu> GetLeftMenu(int UserID)
        {
            var Roleid = dbcontext.tbUSers.Where(m => m.UserID == UserID).FirstOrDefault().RoleId;
            var role = dbcontext.tbRoles.Find(Roleid);
            var getrolemnu = dbcontext.tbRoleMenus.Where(m => m.RoleId == role.RoleId);
            var menu = (from mn in dbcontext.tbMenus.ToList()
                        join rm in getrolemnu.ToList()
                        on mn.MenuId equals rm.MenuId
                        select new tbMenu()
                        {
                            MenuId=mn.MenuId,
                            MenuName=mn.MenuName,
                            MenuURL=mn.MenuURL,
                            STT=mn.STT
                        }
                      );
            return menu;
        }
    }
}
