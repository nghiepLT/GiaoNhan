﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Tool;
using DAL.ViewModels;
using System.Net.Mail;
using System.Web;
namespace DAL
{
    public class DAL_Account
    {
        KpiManagerEntities dbContext = new KpiManagerEntities();
        public tbUSer GetAccountID(string username)
        {
            return dbContext.tbUSers.Where(m => m.UserName == username).FirstOrDefault();
        }
        public bool Login(string user, string passWord)
        {
            var chkLogin = dbContext.tbUSers.ToList().Where(m => m.UserName.ToLower() == user.ToLower() && m.UserPassword == Tool.Helper.Md5(passWord)).FirstOrDefault();
            if (chkLogin != null)
            {

                return true;
            }

            return false;
        }
        public IEnumerable<tbUSer> GetAll()
        {
            return dbContext.tbUSers;
        }
        public IEnumerable<VM_User> GetVMAll(int PermissionID)
        {

            var model = (from us in dbContext.tbUSers
                         join pm in dbContext.tbPermissions
                         on us.PermissionID equals pm.PermissionID
                         join r in dbContext.tbRoles
                         on us.RoleId equals r.RoleId into gj
                         from x in gj.DefaultIfEmpty()
                         where us.Status == 1
                         && ((PermissionID == 0) || (us.PermissionID == PermissionID))
                         select new VM_User()
                         {
                             Description = us.Description,
                             PermissionID = us.PermissionID,
                             PermissionName = pm.PermissionName,
                             UserID = us.UserID,
                             EmployeeName = us.EmployeeName,
                             UserName = us.UserName,
                             Code = us.Code,
                             RoleName = x != null ? x.RoleName : "Chưa phân quyền"
                         }
                       );
            return model;
        }
        public tbUSer GetbyID(int UserID)
        {
            return dbContext.tbUSers.Find(UserID);
        }
        public tbUSer GetbyCode(string Code)
        {
            return dbContext.tbUSers.Where(m=>m.Code==Code).FirstOrDefault();
        }
        public int GetPermissionID(int UserID)
        {
            var getUser = GetbyID(UserID);
            return getUser.PermissionID;
        }
        public IEnumerable<tbUSer> GetByPermissionId(int PermissionID)
        {
            return dbContext.tbUSers.Where(m=>m.PermissionID==PermissionID);
        }
        public int ChangePassword(string UserName,string oldPassword,string newPassword)
        {
            try
            {
                var chkLogin = dbContext.tbUSers.ToList().Where(m => m.UserName == UserName && m.UserPassword == Tool.Helper.Md5(oldPassword)).FirstOrDefault();
                if (chkLogin == null)
                    return 2;
                else
                {
                    chkLogin.UserPassword = Tool.Helper.Md5(newPassword);
                    dbContext.SaveChanges();
                }
                return 1;
            }
            catch(Exception ex)
            {
                return -1;

            } 
        }

        public bool UpdateUser(tbUSer user)
        {
            try
            {
                tbUSer us = dbContext.tbUSers.Find(user.UserID);
                if (us == null)
                {
                    us = new tbUSer();
                    us.UserName = user.UserName;
                    us.EmployeeName = user.EmployeeName;
                    us.UserPassword = Tool.Helper.Md5(user.UserPassword);
                    us.PermissionID = user.PermissionID;
                    us.Code = user.Code;
                    us.Status = 1;
                    us.RoleId = user.RoleId;
                    dbContext.tbUSers.Add(us);
                    dbContext.SaveChanges();
                }
                else
                {
                    us.UserName = user.UserName; 
                    if(dbContext.tbUSers.Find(user.UserID).UserPassword!=user.UserPassword)
                        us.UserPassword = Tool.Helper.Md5(user.UserPassword);
                    us.EmployeeName = user.EmployeeName;
                    us.PermissionID = user.PermissionID;
                    us.Code = user.Code;
                    us.RoleId = user.RoleId;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
          
        }


        public bool DeleteUser(int UserID)
        {

            try
            {
                tbUSer user = dbContext.tbUSers.Find(UserID);
                if (user != null)
                {
                    user.Status = 0;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool CheckUserName(string UserName)
        {
            var chkUser = dbContext.tbUSers.Any(m => m.UserName.ToLower() == UserName.ToLower());
            if (chkUser)
                return true;
            return false;
        }
        public bool CheckSendmailKiemDinh()
        {
            var chkEmail = dbContext.bdSendMails.ToList().Any(m => m.NgayGui.Value.Date == DateTime.Now.Date);
            return chkEmail;
        }
        public IEnumerable<bdCar> GetListCarEmail()
        {
            var lstCar = dbContext.bdCars.ToList().Where(m => m.ThoiGianDangKiem.Value.AddDays(-7).Date == DateTime.Now.Date);
            return lstCar;
        }
        public bool TaoEmailKiemDinh()
        {
            var lstCar = dbContext.bdCars.ToList().Where(m => m.ThoiGianDangKiem.Value.AddDays(-7).Date == DateTime.Now.Date);
            foreach (var item in lstCar)
            {
                bdSendMail bdSendmail = new bdSendMail();
                bdSendmail.IdCar = item.IDCar;
                bdSendmail.Type = 2;
                bdSendmail.NgayGui = DateTime.Now;
                dbContext.bdSendMails.Add(bdSendmail);
                dbContext.SaveChanges();
            }
            return true;
        }


        //Email
        public IEnumerable<bdEmailSend> GetListEmail()
        {
            return dbContext.bdEmailSends.ToList();
        }

        public bdEmailSend GetEmailByID(int IdEmail)
        {
            return dbContext.bdEmailSends.Find(IdEmail);
        }
        public bool InsertEmail(bdEmailSend bdEmailSend)
        {
            try
            {
                if (bdEmailSend.IdEmail == 0)
                {
                    dbContext.bdEmailSends.Add(bdEmailSend);
                    dbContext.SaveChanges();
                }
                else
                {
                    bdEmailSend bdedit = dbContext.bdEmailSends.Find(bdEmailSend.IdEmail);
                    bdedit.Email = bdEmailSend.Email; 
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteEmail(int IdEmail)
        {
            try
            {
                var item = dbContext.bdEmailSends.Find(IdEmail);
                dbContext.bdEmailSends.Remove(item);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
