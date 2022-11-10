using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class VM_Permission_Menu
    {
        public int ID { get; set; }
        public int PermissionID { get; set; }
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public string Icon { get; set; }
        public int STT { get; set; }
    }
}
