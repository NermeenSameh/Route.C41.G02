using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Route.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        /// public ApplicationDbContext() : base(new DbContextOptions<ApplicationDbContext>())
        /// {
        /// 
        /// }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        ///      => optionsBuilder.UseSqlServer("Server = .; Database = MvcApplicationG02; Trusted_Connection  = True; MultipleActiveResultSets = False");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }


        // public DbSet<IdentityUser> Users { get; set; }
        // public DbSet<IdentityRole> Roles { get; set; }



    }
}
