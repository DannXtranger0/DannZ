using DannZ.Models;
using DannZ.Models.Seeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace DannZ.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) :base(options)  
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermission>().HasKey(rp => new {rp.RoleId,rp.PermissionId});

            modelBuilder.ApplyConfiguration(new RoleSeeds());
            modelBuilder.ApplyConfiguration(new PermissionSeeds());
            modelBuilder.ApplyConfiguration(new RolesPermissionsSeeds());

        }

    }
}
