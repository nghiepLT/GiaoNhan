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
    
    public partial class tbTracking
    {
        public int TrackingID { get; set; }
        public string TrackingCode { get; set; }
        public System.DateTime TrackingDate { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> DateStart { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public int Count { get; set; }
        public Nullable<int> KPI { get; set; }
        public int CountStep { get; set; }
    }
}