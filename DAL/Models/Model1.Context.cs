﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KpiManagerEntities : DbContext
    {
        public KpiManagerEntities()
            : base("name=KpiManagerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbConfig> tbConfigs { get; set; }
        public virtual DbSet<tbDepartment> tbDepartments { get; set; }
        public virtual DbSet<tbDriver> tbDrivers { get; set; }
        public virtual DbSet<tbEmployee> tbEmployees { get; set; }
        public virtual DbSet<tbMenu> tbMenus { get; set; }
        public virtual DbSet<tbNCC> tbNCCs { get; set; }
        public virtual DbSet<tbPermission> tbPermissions { get; set; }
        public virtual DbSet<tbPermission_Menu> tbPermission_Menu { get; set; }
        public virtual DbSet<tbProduct> tbProducts { get; set; }
        public virtual DbSet<tbReceived> tbReceiveds { get; set; }
        public virtual DbSet<tbReceivedDetail> tbReceivedDetails { get; set; }
        public virtual DbSet<tbUSer> tbUSers { get; set; }
        public virtual DbSet<tbTheTai> tbTheTais { get; set; }
        public virtual DbSet<tbTheTaiChiTiet> tbTheTaiChiTiets { get; set; }
        public virtual DbSet<tbTracking> tbTrackings { get; set; }
        public virtual DbSet<tbRole> tbRoles { get; set; }
        public virtual DbSet<tbRoleMenu> tbRoleMenus { get; set; }
        public virtual DbSet<bdCar> bdCars { get; set; }
        public virtual DbSet<bdEmailSend> bdEmailSends { get; set; }
        public virtual DbSet<bdTaixe> bdTaixes { get; set; }
        public virtual DbSet<bdTypeCar> bdTypeCars { get; set; }
        public virtual DbSet<bdService> bdServices { get; set; }
        public virtual DbSet<bdDotbaoduong> bdDotbaoduongs { get; set; }
        public virtual DbSet<bdHanhtrinhbaotri> bdHanhtrinhbaotris { get; set; }
    }
}
