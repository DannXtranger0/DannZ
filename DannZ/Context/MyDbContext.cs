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
        public DbSet<Post> Posts { get; set; }
        public DbSet<MediaContent> MediaContent { get; set; }
        public DbSet<CommentPost> CommentPosts { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relationship n to n bettwen users, posts and comments
            modelBuilder.Entity<CommentPost>()
                .HasOne(cp => cp.Post)
                .WithMany(p => p.Comment_Posts)
                .HasForeignKey(cp => cp.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CommentPost>()
                .HasOne(cp=> cp.User)
                .WithMany(u=> u.CommentsPosts)
                .HasForeignKey(cp=>cp.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            //Relationship n to 1 between posts and users
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

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
