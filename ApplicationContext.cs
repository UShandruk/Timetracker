using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timetracker
{
    using Microsoft.EntityFrameworkCore;

    namespace RolesApp.Models
    {
        public class ApplicationContext : DbContext
        {
            public DbSet<Employee> Employees { get; set; }
            public DbSet<Role> Roles { get; set; }
            public ApplicationContext(DbContextOptions<ApplicationContext> options)
                : base(options)
            {
                Database.EnsureCreated();
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                string adminRoleName = "admin";
                string userRoleName = "user";
                string blockedRoleName = "blocked";

                string adminLogin = "admin";
                string adminPassword = "12345678";

                //string userLogin = "user";
                //string userPassword = "12345678";

                // добавляем роли
                Role adminRole = new Role { Id = 1, Name = adminRoleName };
                Role userRole = new Role { Id = 2, Name = userRoleName };
                Role blockedRole = new Role { Id = 3, Name = blockedRoleName };
                Employee adminEmployee = new Employee { Id = 1, Login = adminLogin, Password = adminPassword, RoleId = adminRole.Id };

                modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole, blockedRole });
                modelBuilder.Entity<Employee>().HasData(new Employee[] { adminEmployee });
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
