using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DannZ.Models.Seeds
{
    public class PostSeeds : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(
                new Post { Id=1,TextContent="Ey yo, im the hacker!",UploadedDateTime=DateTime.Parse("7-16-2025"),UserId=1},
                new Post { Id=2,TextContent="Ey yo, im NOT the hacker!",UploadedDateTime = DateTime.Parse("7-16-2025"),UserId=2 },
                new Post { Id=3,TextContent="Ey yo, asdasdasdasdim NOT the hacker!",UploadedDateTime = DateTime.Parse("7-16-2025"), UserId= 1 },
                new Post { Id=4,TextContent="Ey yo, asdasdasdasdim NOT the sdasdasdashasdasdacker!",UploadedDateTime= DateTime.Parse("7-16-2025"), UserId= 2 },
                new Post { Id=5,TextContent="Ey yo, fuck the police!",UploadedDateTime = DateTime.Parse("7-16-2025"), UserId= 1 }
                );
        }
    }
}
