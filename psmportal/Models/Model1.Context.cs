﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace psmportal.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class db_psmportalEntities1 : DbContext
    {
        public db_psmportalEntities1()
            : base("name=db_psmportalEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tb_committee> tb_committee { get; set; }
        public virtual DbSet<tb_domain> tb_domain { get; set; }
        public virtual DbSet<tb_evaluation> tb_evaluation { get; set; }
        public virtual DbSet<tb_lecturer> tb_lecturer { get; set; }
        public virtual DbSet<tb_program> tb_program { get; set; }
        public virtual DbSet<tb_proposal> tb_proposal { get; set; }
        public virtual DbSet<tb_request> tb_request { get; set; }
        public virtual DbSet<tb_status> tb_status { get; set; }
        public virtual DbSet<tb_student> tb_student { get; set; }
        public virtual DbSet<tb_sv> tb_sv { get; set; }
        public virtual DbSet<tb_user> tb_user { get; set; }
        public virtual DbSet<tb_evaluator> tb_evaluator { get; set; }
    }
}
