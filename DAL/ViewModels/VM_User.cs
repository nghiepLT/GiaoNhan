using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class VM_User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Description { get; set; }
        public int PermissionID { get; set; }
        public string PermissionName { get; set; } 
        public int ? EmployeeID { get; set; }
        public string EmployeeName { get; set; } 
        public string Code { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
