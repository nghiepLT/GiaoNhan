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


    public class BAL_Employee
    {
        DAL_Employee employee = new DAL_Employee();

        public bool EmployeeUpdate(int EmployeeID, string EmployeeName, int PermissionID)
        {
            return employee.EmployeeUpdate(EmployeeID, EmployeeName, PermissionID); 
        }
        public IEnumerable<tbEmployee> GetAll()
        {
            return employee.GetAll();
        }
        public IEnumerable<tbEmployee> GetAllRegister()
        {
            return employee.GetAllRegister();
        }
        public IEnumerable<VM_Employee> GetAllVM()
        {
            return employee.GetAllVM();
        }
        public tbEmployee GetById(int EmployeeID)
        {
            return employee.GetById(EmployeeID);
        }
        public int GetPermissionID(int EmployeeID)
        {
            return employee.GetPermissionID(EmployeeID);
        }
    }
}
