using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmsRanking.Migrations
{
    /// <inheritdoc />
    public partial class ImplementedCloudinarySevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "MediaContent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "AppUser",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "MediaContent");

            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "AppUser");
        }
    }
}
