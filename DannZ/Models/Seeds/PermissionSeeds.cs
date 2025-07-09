using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DannZ.Models.Seeds
{
    public class PermissionSeeds : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(
                new Permission {Id = 1, PermissionName = "CanComment"},
                new Permission {Id = 2, PermissionName = "CanPost" },
                new Permission {Id = 3, PermissionName = "CanReact" },
                new Permission {Id = 4, PermissionName = "IsAdmin" }
                );
        }
    }
}
