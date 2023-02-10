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
    public class BAL_User
    {
        DAL_Account account = new DAL_Account();


        public tbUSer GetAccountID(string username)
        {
            return account.GetAccountID(username);
        }
        public bool Login(string user, string passWord)
        {
            return account.Login(user, passWord);
        }
        public IEnumerable<tbUSer> GetAll()
        {
            return account.GetAll();
        }
        public IEnumerable<VM_User> GetVMAll(int PermissionID)
        {

            return account.GetVMAll(PermissionID);
        }
        public tbUSer GetbyID(int UserID)
        {
            return account.GetbyID(UserID);
        }
        public tbUSer GetbyCode(string Code)
        {
            return account.GetbyCode(Code);
        }
        public int GetPermissionID(int UserID)
        {
            return account.GetPermissionID(UserID);
        }
        public IEnumerable<tbUSer> GetByPermissionId(int PermissionID)
        {
            return account.GetByPermissionId(PermissionID);
        }

        public int ChangePassword(string UserName, string oldPassword, string newPassword)
        {
            return account.ChangePassword(UserName, oldPassword, newPassword);
        }

        public bool UpdateUser(tbUSer user)
        {
            return account.UpdateUser(user);
        }
        public bool DeleteUser(int UserID)
        {
            return account.DeleteUser(UserID);
        }
        public bool CheckUserName(string UserName)
        {
            return account.CheckUserName(UserName);
        }
        public bool CheckSendmailKiemDinh()
        {
            return account.CheckSendmailKiemDinh();
        }
        public bool TaoEmailKiemDinh()
        {
            return account.TaoEmailKiemDinh();
        }
        public IEnumerable<bdCar> GetListCarEmail()
        {
            return account.GetListCarEmail();
        }
    }
}
