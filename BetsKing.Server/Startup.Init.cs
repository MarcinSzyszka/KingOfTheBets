using BetsKing.Server.Data.Context;
using BetsKing.Server.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BetsKing.Server
{
    public partial class Startup
    {
        public void InitDev(BetsKingDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            InitData(dbContext);
        }

        public void InitProd(BetsKingDbContext dbContext)
        {
            dbContext.Database.Migrate();
            InitData(dbContext);
        }

        private void InitData(BetsKingDbContext dbContext)
        {
            if (!dbContext.Roles.Any())
            {
                dbContext.Roles.Add(new Role
                {
                    Name = "Gambler"
                });

                dbContext.Roles.Add(new Role
                {
                    Name = "Admin"
                });

                dbContext.SaveChanges();
            }

            if (dbContext.Gamblers.FirstOrDefault(g => g.UserName.Equals(Configuration["DbInit:AdminUserName"], StringComparison.InvariantCultureIgnoreCase)) == null)
            {
                var admin = new Gambler
                {
                    UserName = Configuration["DbInit:AdminUserName"],
                    DisplayName = Configuration["DbInit:AdminDisplayName"]
                };

                var passwordHasher = new PasswordHasher<Gambler>();

                admin.Password = passwordHasher.HashPassword(admin, Configuration["DbInit:AdminPassword"]);

                var entry = dbContext.Gamblers.Add(admin);

                dbContext.SaveChanges();

                dbContext.UserRoles.AddRange(new UserRole
                {
                    GamblerId = entry.Entity.Id,
                    RoleId = 1
                },
                new UserRole
                {
                    GamblerId = entry.Entity.Id,
                    RoleId = 2
                });

                dbContext.SaveChanges();
            }
        }
    }
}
