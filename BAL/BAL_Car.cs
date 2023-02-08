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
    public class BAL_Car
    {
        DAL_Car dalCar = new DAL_Car();
        public IEnumerable<bdTypeCar> GetListTypeCar()
        {
            return dalCar.GetListTypeCar();
        }
        public bool InsertTypeCar(bdTypeCar bdTypeCar)
        {
            return dalCar.InsertTypeCar(bdTypeCar);
        }
        public bdTypeCar GetTypeCarByID(int TypeCar)
        {
            return dalCar.GetTypeCarByID(TypeCar);
        }
        public bool DeleteTypeCar(int TypeCar)
        {
            return dalCar.DeleteTypeCar(TypeCar);
        }

        //Tai xe
        public IEnumerable<bdTaixe> GetListTaiXe()
        {
            return dalCar.GetListTaiXe();
        }
        public bdTaixe GetTaiXeByID(int IdTaixe)
        {
            return dalCar.GetTaiXeByID(IdTaixe);
        }
        public bool InsertTaiXe(bdTaixe bdTaixe)
        {
            return dalCar.InsertTaiXe(bdTaixe);
        }
        public bool DeleteTaiXe(int IdTaixe)
        {
            return dalCar.DeleteTaiXe(IdTaixe);
        }
            //Car
        public IEnumerable<VM_Car> GetListCar()
        {
            return dalCar.GetListCar();
        }
        public IEnumerable<VM_Car> GetListCarByID(int TypeCar)
        {
            return dalCar.GetListCarByID(TypeCar);
        }
        public bdCar GetCarByID(int IDCar)
        {
            return dalCar.GetCarByID(IDCar);
        }
        public bool InsertCar(bdCar bdCar)
        {
            return dalCar.InsertCar(bdCar);
        }
        public bool DeleteCar(int IDCar)
        {
            return dalCar.DeleteCar(IDCar);
        }
        public bool InsertDotBaoDuong(int IDCar, DateTime NgayBD, int SokmDau)
        {
            return dalCar.InsertDotBaoDuong(IDCar, NgayBD, SokmDau);
        }
        public VM_DotBaoDuong GetDotBaoDuong(int IDCar)
        {
            return dalCar.GetDotBaoDuong(IDCar);
        }
        public IEnumerable<bdHanhtrinhbaotri> GetHanhtrinhbaotri(int IDCar)
        {
            return dalCar.GetHanhtrinhbaotri(IDCar);
        }
        public bool CapnhatsoKM(int IDCar, int SoKM)
        {
            return dalCar.CapnhatsoKM(IDCar, SoKM);
        }
        public bool DeleteHanhtrinh(int IDHanhTrinhBaoTri)
        {
            return dalCar.DeleteHanhtrinh(IDHanhTrinhBaoTri);
        }
    }
}
