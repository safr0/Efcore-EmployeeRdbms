using EfCoreConsole.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreConsole
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> EmployeeList { get; set; }
        public DbSet<EmployeeManagerRelation> EmployeeManagerList { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                modelBuilder.Entity<Employee>()
                    .HasKey(d => d.EmployeeID);

                modelBuilder.Entity<Employee>()
                    .Property(d => d.EmployeeID)
                    .ValueGeneratedNever();                

                modelBuilder.Entity<EmployeeManagerRelation>()
                    .HasKey(x => new { EmployeeID = x.EmployeeID });

                modelBuilder.Entity<EmployeeManagerRelation>()
                    .Property(d => d.EmployeeID)
                    .ValueGeneratedNever();
                modelBuilder.Entity<EmployeeManagerRelation>()
                    .Property(d => d.ManagerID)
                    .ValueGeneratedNever();

                modelBuilder.Entity<EmployeeManagerRelation>()
                    .HasOne(x => x.Employee)
                    .WithMany(x => x.EmployeeList)
                    .HasForeignKey(x => x.EmployeeID)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<EmployeeManagerRelation>()
                    .HasOne(x => x.Manager)
                    .WithMany(x => x.Managers)
                    .HasForeignKey(x => x.ManagerID)
                    .OnDelete(DeleteBehavior.Restrict);
            });            

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EmployeeDB;Trusted_Connection=True;");
        }
    }
}
