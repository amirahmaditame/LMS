﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace DataLayer
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class LearningDBEntities : DbContext
{
    public LearningDBEntities()
        : base("name=LearningDBEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Categories> Categories { get; set; }

    public virtual DbSet<Courses> Courses { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Selected_Category> Selected_Category { get; set; }

    public virtual DbSet<SubCourse> SubCourse { get; set; }

    public virtual DbSet<Teachers_PF> Teachers_PF { get; set; }

    public virtual DbSet<User_PF> User_PF { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<Discount> Discount { get; set; }

    public virtual DbSet<Notifications> Notifications { get; set; }

}

}

