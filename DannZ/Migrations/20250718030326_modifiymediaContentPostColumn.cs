using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DannZ.Migrations
{
    /// <inheritdoc />
    public partial class modifiymediaContentPostColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isVideo",
                table: "MediaContentPost");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isVideo",
                table: "MediaContentPost",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
