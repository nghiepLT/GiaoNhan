//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class bdCar
    {
        public int IDCar { get; set; }
        public string CarSignal { get; set; }
        public Nullable<int> IdTypeCar { get; set; }
        public Nullable<int> IdTaiXe { get; set; }
        public Nullable<System.DateTime> ThoiGianDangKiem { get; set; }
    }
}