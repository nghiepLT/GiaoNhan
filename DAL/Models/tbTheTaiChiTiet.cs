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
    
    public partial class tbTheTaiChiTiet
    {
        public int TheTaiChiTietID { get; set; }
        public Nullable<int> ThetaiID { get; set; }
        public string MaPhieu { get; set; }
        public Nullable<int> Status { get; set; }
        public string MaThe { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public string GhiChuKetThuc { get; set; }
        public Nullable<int> TienPhatSinh { get; set; }
        public Nullable<int> SoKMPhatSinh { get; set; }
    }
}
