using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.ViewModels;
namespace DAL
{
    public class DAL_Employee
    {
        KpiManagerEntities dbcontext = new KpiManagerEntities();


        public IEnumerable<tbEmployee> GetAll()
        {
            return dbcontext.tbEmployees;
        }
        public IEnumerable<tbEmployee> GetAllRegister()
        {
            return null;
        }
        public IEnumerable<VM_Employee> GetAllVM()
        {
            var model = (from us in dbcontext.tbEmployees
                         join pm in dbcontext.tbPermissions
                         on us.PermissionID equals pm.PermissionID
                         select new VM_Employee()
                         {
                             EmployeeID=us.EmployeeID,
                             EmployeeName=us.EmployeeName,
                             PermissionID=us.PermissionID,
                             PermissionName=pm.PermissionName
                         }

                       );
            return model;
        }
        public bool EmployeeUpdate(int EmployeeID, string EmployeeName, int PermissionID)
        {
            try
            {
                tbEmployee employee = dbcontext.tbEmployees.Find(EmployeeID);
                if (employee == null)
                {
                    employee = new tbEmployee();
                    employee.EmployeeName = EmployeeName;
                    employee.PermissionID = PermissionID;
                    dbcontext.tbEmployees.Add(employee);
                    dbcontext.SaveChanges();
                }
                else
                {
                    employee.EmployeeName = EmployeeName;
                    employee.PermissionID = PermissionID;
                    dbcontext.SaveChanges();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
        public tbEmployee GetById(int EmployeeID)
        {
            return dbcontext.tbEmployees.Find(EmployeeID);
        }
        public int GetPermissionID(int EmployeeID)
        {
            return dbcontext.tbEmployees.Where(m => m.EmployeeID == EmployeeID).FirstOrDefault().PermissionID.Value;
        }
    }
}
