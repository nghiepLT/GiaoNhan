using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Tool;
using DAL.ViewModels;
namespace DAL
{
    public class DAL_Car
    {
        KpiManagerEntities dbContext = new KpiManagerEntities();

        public IEnumerable<bdTypeCar> GetListTypeCar()
        {
            return dbContext.bdTypeCars.ToList();
        }

        public bdTypeCar GetTypeCarByID(int TypeCar)
        {
            return dbContext.bdTypeCars.Find(TypeCar);
        }
        public bool InsertTypeCar(bdTypeCar bdTypeCar)
        {
            try
            {
                if(bdTypeCar.TypeCar==0)
                {
                    dbContext.bdTypeCars.Add(bdTypeCar);
                    dbContext.SaveChanges();
                }
                else
                {
                    bdTypeCar bdedit = dbContext.bdTypeCars.Find(bdTypeCar.TypeCar);
                    bdedit.NameType = bdTypeCar.NameType;
                    bdedit.DinhMucBaoDuong = bdTypeCar.DinhMucBaoDuong;
                    bdedit.NameType = bdTypeCar.NameType;
                    bdedit.Ghichubaoduong = bdTypeCar.Ghichubaoduong;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool DeleteTypeCar(int TypeCar)
        {
            try
            {
                var item = dbContext.bdTypeCars.Find(TypeCar);
                dbContext.bdTypeCars.Remove(item);
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        //Tài xế
        public IEnumerable<bdTaixe> GetListTaiXe()
        {
            return dbContext.bdTaixes.ToList();
        }
        public bdTaixe GetTaiXeByID(int IdTaixe)
        {
            return dbContext.bdTaixes.Find(IdTaixe);
        }
        public bool InsertTaiXe(bdTaixe bdTaixe)
        {
            try
            {
                if (bdTaixe.IdTaixe == 0)
                {
                    dbContext.bdTaixes.Add(bdTaixe);
                    dbContext.SaveChanges();
                }
                else
                {
                    bdTaixe bdedit = dbContext.bdTaixes.Find(bdTaixe.IdTaixe);
                    bdedit.TenTaiXe = bdTaixe.TenTaiXe;
                    bdedit.MaTheTai = bdTaixe.MaTheTai;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteTaiXe(int IdTaixe)
        {
            try
            {
                var item = dbContext.bdTaixes.Find(IdTaixe);
                dbContext.bdTaixes.Remove(item);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //Car
        public IEnumerable<VM_Car> GetListCar()
        {
            var model = (from c in dbContext.bdCars.ToList()
                         join tx in dbContext.bdTaixes
                         on c.IdTaiXe equals tx.IdTaixe
                         join tc in dbContext.bdTypeCars
                         on c.IdTypeCar equals tc.TypeCar 

                         select new VM_Car()
                         {
                             CarSignal = c.CarSignal,
                             IDCar = c.IDCar,
                             MaTheTai = tx.MaTheTai,
                             NameType = tc.NameType,
                             TenTaiXe = tx.TenTaiXe,
                             DinhMucBaoDuong=tc.DinhMucBaoDuong.Value,
                             SoKmHientai = GetSoKMHientai(c.IDCar)
                         }
                       ).ToList();
            return model;
        }

        public int GetSoKMHientai(int IDCar)
        {
            bdDotbaoduong bdDotbaoduong = dbContext.bdDotbaoduongs.OrderByDescending(m => m.IdDotBaoDuong).Where(m => m.IDCar == IDCar).FirstOrDefault();
            if (bdDotbaoduong == null)
                return 0;
            return bdDotbaoduong.SoKmHientai.Value;
        }
        public IEnumerable<VM_Car> GetListCarByID(int TypeCar)
        {
            var model = (from c in dbContext.bdCars
                         join tx in dbContext.bdTaixes
                         on c.IdTaiXe equals tx.IdTaixe
                         join tc in dbContext.bdTypeCars
                         on c.IdTypeCar equals tc.TypeCar
                         where c.IdTypeCar== TypeCar
                         select new VM_Car()
                         {
                             CarSignal = c.CarSignal,
                             IDCar = c.IDCar,
                             MaTheTai = tx.MaTheTai,
                             NameType = tc.NameType,
                             TenTaiXe = tx.TenTaiXe,
                             ThoiGianDangKiem=c.ThoiGianDangKiem.Value
                         }
                       ).ToList();
            return model;
        }
        public bdCar GetCarByID(int IDCar)
        {
            return dbContext.bdCars.Find(IDCar);
        }
        public bool InsertCar(bdCar bdCar)
        {
            try
            {
                if (bdCar.IDCar == 0)
                {
                    dbContext.bdCars.Add(bdCar);
                    dbContext.SaveChanges();
                }
                else
                {
                    bdCar bdedit = dbContext.bdCars.Find(bdCar.IDCar);
                    bdedit.IdTypeCar = bdCar.IdTypeCar;
                    bdedit.IdTaiXe = bdCar.IdTaiXe;
                    bdedit.CarSignal = bdCar.CarSignal;
                    bdedit.ThoiGianDangKiem = bdCar.ThoiGianDangKiem;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCar(int IDCar)
        {
            try
            {
                var item = dbContext.bdCars.Find(IDCar);
                dbContext.bdCars.Remove(item);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //Đợt bảo dưỡng

        public bool InsertDotBaoDuong(int IDCar, DateTime NgayBD, int SokmDau)
        {
            try
            {
                //Cập nhật cho thằng trước
                bdDotbaoduong bdbdbefore = dbContext.bdDotbaoduongs.Where(m => m.IDCar == IDCar).ToList().LastOrDefault();
                if (bdbdbefore != null)
                {
                    bdbdbefore.SoKmCuoi = SokmDau;
                    bdbdbefore.NgayKT = NgayBD;
                    dbContext.SaveChanges();
                }
               
                bdDotbaoduong bdbd = new bdDotbaoduong();
                bdbd = new bdDotbaoduong();
                bdbd.IDCar = IDCar;
                bdbd.SoKmHientai = 0;
                bdbd.NgayBD = NgayBD;
                bdbd.SokmDau = SokmDau;
                dbContext.bdDotbaoduongs.Add(bdbd);
               
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public VM_DotBaoDuong GetDotBaoDuong(int IDCar)
        {
            var model = (from d in dbContext.bdDotbaoduongs 
                         join c in dbContext.bdCars
                         on d.IDCar equals c.IDCar
                         join tx in dbContext.bdTaixes
                         on c.IdTaiXe equals tx.IdTaixe
                         join tc in dbContext.bdTypeCars
                         on c.IdTypeCar equals tc.TypeCar
                         where d.IDCar==IDCar
                         select new VM_DotBaoDuong()
                         {
                             IDCar = d.IDCar,
                             IdDotBaoDuong = d.IdDotBaoDuong,
                             NgayBD = d.NgayBD,
                             NgayKT = d.NgayKT,
                             SoKmCuoi = d.SoKmCuoi,
                             SokmDau = d.SokmDau,
                             SoKmHientai = d.SoKmHientai,
                             STT = dbContext.bdDotbaoduongs.Where(m=>m.IDCar==IDCar).Count(),
                             DinhMucBaoDuong=tc.DinhMucBaoDuong.Value,
                             Chisodukienlansau=d.SokmDau.Value+tc.DinhMucBaoDuong.Value,
                             Sokmconlai= tc.DinhMucBaoDuong.Value-d.SoKmHientai.Value ,
                             TenTaiXe=tx.TenTaiXe
                         }
                       ).ToList().LastOrDefault();
            return model;
        }
        public IEnumerable<bdHanhtrinhbaotri> GetHanhtrinhbaotri(int IDCar)
        {
            bdDotbaoduong lastDotbaoduong = dbContext.bdDotbaoduongs.OrderByDescending(m=>m.IdDotBaoDuong).Where(m => m.IDCar == IDCar).FirstOrDefault();
            var model = lastDotbaoduong!=null? dbContext.bdHanhtrinhbaotris.Where(m => m.IDDotbaoduong == lastDotbaoduong.IdDotBaoDuong).OrderByDescending(m=>m.IDHanhTrinhBaoTri).ToList():null;
            return model;
        }
        public bool CapnhatsoKM(int IDCar,int SoKM)
        {
            try
            {
                bdDotbaoduong dbd = dbContext.bdDotbaoduongs.OrderByDescending(m => m.IdDotBaoDuong).Where(m => m.IDCar == IDCar).FirstOrDefault();
                if (dbd != null)
                {
                    dbd.SoKmHientai = SoKM;
                    dbContext.SaveChanges();
                    //Insert history
                    bdHanhtrinhbaotri ht = new bdHanhtrinhbaotri();
                    ht.IDDotbaoduong = dbd.IdDotBaoDuong;
                    ht.Sokm = SoKM;
                    ht.NgayTao = DateTime.Now;
                    dbContext.bdHanhtrinhbaotris.Add(ht);
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool CheckSendEmail(int IDcar,int SoKM)
        {
            var lastbd = dbContext.bdDotbaoduongs.Where(m => m.IDCar == IDcar).ToList().LastOrDefault();
            var car = dbContext.bdCars.Where(m => m.IDCar == IDcar).FirstOrDefault();
            var typecar = dbContext.bdTypeCars.Where(m => m.TypeCar == car.IdTypeCar).FirstOrDefault();
            var conlai = typecar.DinhMucBaoDuong - SoKM;
            if (conlai <= typecar.Ghichubaoduong)
                return true;
            return false;
        }
        public bool DeleteHanhtrinh(int IDHanhTrinhBaoTri)
        {
            try
            {
                bdHanhtrinhbaotri ht = dbContext.bdHanhtrinhbaotris.Where(m => m.IDHanhTrinhBaoTri == IDHanhTrinhBaoTri).FirstOrDefault();
                bdHanhtrinhbaotri lastht = dbContext.bdHanhtrinhbaotris.ToList().Where(m => m.IDHanhTrinhBaoTri != ht.IDHanhTrinhBaoTri && m.IDDotbaoduong==ht.IDDotbaoduong).LastOrDefault();
                bdDotbaoduong dbd;
                dbd=dbContext.bdDotbaoduongs.Where(m => m.IdDotBaoDuong == ht.IDDotbaoduong).FirstOrDefault();
                dbd.SoKmHientai = lastht!=null? lastht.Sokm:0;
                dbContext.bdHanhtrinhbaotris.Remove(ht);
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
