using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DannZ.Models.Seeds
{
    public class UserSeeds : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User { Id = 1, Name = "Dann", Email = "dann@gmail.com", Biography = "I'm The Sex Lord", Password= "$2a$11$UHAmY3s8g/F9yMfbtELVNu6zdv1lIsEaW9eP5ph13NmiNi2debsE2",RoleId=2 },
                new User { Id = 2, Name = "Robin", Email = "robin@gmail.com", Biography = "I'm NOT The Sex Lord", Password= "$2a$11$UHAmY3s8g/F9yMfbtELVNu6zdv1lIsEaW9eP5ph13NmiNi2debsE2",RoleId=2 }
                );
        }
    }
}
