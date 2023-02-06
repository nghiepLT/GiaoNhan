using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
    public static class Helper
    {

        public static string Md5(string source)
        {
            using (var md5Hash = MD5.Create())
            {
                // Byte array representation of source string
                var sourceBytes = Encoding.UTF8.GetBytes(source);

                // Generate hash value(Byte Array) for input data
                var hashBytes = md5Hash.ComputeHash(sourceBytes);

                // Convert hash byte array to string
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                // Output the MD5 hash
                return hash;
            }
        }
        public static string ReturnTime(int Time)
        {
            var strTime = "";
            //Tính theo giờ
            if (Time>=3600)
            {
                var sogio = Time / 3600;
                var tongsophut = Time - (sogio * 3600);
                var sophut = tongsophut / 60;
                var sogiaydu = tongsophut - sophut * 60;
                strTime =sogio+" Giờ "+ sophut + " phút " + sogiaydu + " giây ";
            }
            else
            {
                //Tính theo phút
                if (Time >= 60)
                {
                    var sophut = Time / 60;
                    var giaydu = Time - (sophut * 60);
                    strTime = sophut + " phút " + giaydu + " giây "; 
                }
                //Tính theo giây
                else
                {
                    strTime = Time + " giây";
                }
            }
            
            return strTime;
        }
        public static string ReturnTime(double Time)
        {
            var strTime = "";
            //Tính theo giờ
            if (Time >= 3600)
            {
                var sogio = Time / 3600;
                var tongsophut = Time - (sogio * 3600);
                var sophut = tongsophut / 60;
                var sogiaydu = tongsophut - sophut * 60;
                strTime = sogio + " Giờ " + sophut + " phút " + sogiaydu + " giây ";
            }
            else
            {
                //Tính theo phút
                if (Time >= 60)
                {
                    var sophut = Time / 60;
                    var giaydu = Time - (sophut * 60);
                    strTime = sophut + " phút " + giaydu + " giây ";
                }
                //Tính theo giây
                else
                {
                    strTime = Time + " giây";
                }
            }

            return strTime;
        }
        public static string Tinhtongthoigian(DateTime dt1,DateTime dat2)
        {
            if (dt1.Date != DateTime.MinValue && dat2.Date!=DateTime.MinValue)
            {
                var total = dt1 - dat2;
                var time = Math.Round(total.TotalSeconds);
                var returntime = ReturnTime(int.Parse(time.ToString()));
                return returntime;
            }
            return "";
        }
    }
}