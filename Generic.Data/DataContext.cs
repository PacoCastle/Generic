using Generic.Core;
using Generic.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Generic.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Client> Client { get; set; }

        public DbSet<Menu> Menu { get; set; }

        public DbSet<RoleMenu> RoleMenu { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();


                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<RoleMenu>(roleMenu =>
            {
                roleMenu.HasKey(rm => new { rm.RoleId, rm.MenuId });

                roleMenu.HasOne(rm => rm.Role)
                    .WithMany(r => r.RoleMenus)
                    .HasForeignKey(rm => rm.RoleId)
                    .IsRequired();

                roleMenu.HasOne(rm => rm.Menu)
                    .WithMany(m => m.RoleMenus)
                    .HasForeignKey(rm => rm.MenuId)
                    .IsRequired();
            });
        }
    }
}
