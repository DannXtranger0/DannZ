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
        public DbSet<UserProfileImages> UserProfileImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermission>().HasKey(rp => new {rp.RoleId,rp.PermissionId});
            //Relationship 1 to 1 between userProfileImages and User
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserProfileImages)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfileImages>(p => p.UserId);

            modelBuilder.ApplyConfiguration(new RoleSeeds());
            modelBuilder.ApplyConfiguration(new PermissionSeeds());
            modelBuilder.ApplyConfiguration(new RolesPermissionsSeeds());

        }

    }
}
