using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class VM_Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int ? PermissionID { get; set; }
        public string PermissionName { get; set; }
    }
}
