using DannZ.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DannZ.Models.Seeds
{
    public class RolesPermissionsSeeds : IEntityTypeConfiguration<RolePermission>
    {
        public  void Configure(EntityTypeBuilder<RolePermission> builder)
        {//User = 2
            builder.HasData(
                new RolePermission { RoleId = 2, PermissionId=1},    
                new RolePermission { RoleId = 2, PermissionId=2},   
                new RolePermission { RoleId = 2, PermissionId=3 }
            );
        }
    }
}
