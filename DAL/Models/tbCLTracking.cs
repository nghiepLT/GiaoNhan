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
    
    public partial class tbCLTracking
    {
        public int CLTrackingId { get; set; }
        public string TrackingCode { get; set; }
        public Nullable<System.DateTime> TrackingDate { get; set; }
        public Nullable<int> UserId { get; set; }
    }
}
