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
    
    public partial class tbReceived
    {
        public int ReceivedID { get; set; }
        public Nullable<int> STT { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> NCCID { get; set; }
        public System.DateTime DateStart { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> Kpi { get; set; }
        public Nullable<int> SLNhap { get; set; }
        public Nullable<int> SlKiemTra { get; set; }
        public string Products { get; set; }
    }
}
