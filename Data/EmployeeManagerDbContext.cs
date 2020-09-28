using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.WebAPI.Data
{
    public class EmployeeManagerDbContext : DbContext
    {
        public EmployeeManagerDbContext(DbContextOptions<EmployeeManagerDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //store gender enum as string
            builder.Entity<Employee>()
                .Property(e => e.Gender)
                .HasConversion<string>();
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<JobCategory> JobCategories { get; set; }
    }
}
