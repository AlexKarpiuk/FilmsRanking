using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmsRanking.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPropertienInMediaContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Director",
                table: "MediaContent",
                newName: "Overview");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Overview",
                table: "MediaContent",
                newName: "Director");
        }
    }
}
