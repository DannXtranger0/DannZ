using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DannZ.Models.Seeds
{
    public class RoleSeeds : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role { Id =1, RoleName ="Admin"},
                new Role { Id =2, RoleName ="User"},
                new Role { Id =3, RoleName ="Guest"}
            );
        }
    }
}
