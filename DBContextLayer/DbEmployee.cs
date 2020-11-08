using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using WEBAPI.Models;



namespace WEBAPI.DBContextLayer
{
    public class DbEmployee : DbContext
    {
        

        public DbEmployee() : base("name=DEMOWEB")
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // cau hinh employee
            modelBuilder.Entity<Employee>().HasKey(k => k.ID);

            modelBuilder.Entity<Employee>().Property(p => p.ID)
            .HasColumnType("char")
            .HasMaxLength(10);

            modelBuilder.Entity<Employee>().Property(p => p.Name)
            
            .HasMaxLength(30);
           

            modelBuilder.Entity<Employee>().Property(p => p.Addrees)
           
            .HasMaxLength(30);

            // cau hinh Departmebt

            modelBuilder.Entity<Department>().HasKey(k => k.IDDE);

            modelBuilder.Entity<Department>().Property(p => p.IDDE)
            .HasColumnType("char")
            .HasMaxLength(5);

            modelBuilder.Entity<Department>().Property(p => p.Name)

            .HasMaxLength(30);

        }

        
    }
}