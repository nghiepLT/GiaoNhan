using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
namespace DAL.ViewModels
{
   public class VM_Role
    { 
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<tbMenu> lsttbMenu { get; set; } = new List<tbMenu>();
    }
}
