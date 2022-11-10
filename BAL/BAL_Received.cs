﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using DAL.ViewModels;
namespace BAL
{

    public class BAL_Received
    {
        DAL_Received received = new DAL_Received();
        public IEnumerable<tbReceived> GetAll()
        {
            return received.GetAll();
        }
        public IEnumerable<VM_Received> GetAllByIDUser(int UserID)
        {
            return received.GetAllByIDUser(UserID);
        }
        public tbReceived GetById(int ReceivedID)
        {
            return received.GetById(ReceivedID);
        }
        public bool Insert(tbReceived tbReceived,int UserID)
        {
            try
            {
                received.Insert(tbReceived, UserID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool Update(int ReceivedID,int NCCID, int Type, List<tbReceivedDetail> list)
        {
            return received.Update(ReceivedID,NCCID, Type, list);
        }
        public IEnumerable<tbNCC> GetNCC()
        {
            return received.GetNCC();
        }
        public IEnumerable<tbProduct> GetProduct()
        {
            return received.GetProduct();
        }
        public bool DeleteRecived(int ReceivedID)
        {
            return received.DeleteRecived(ReceivedID);
        }
        public VM_Received GetPopupInfo(int ReceivedID)
        {
            return received.GetPopupInfo(ReceivedID);
        }
    }
}