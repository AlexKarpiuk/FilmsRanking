using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmsRanking.Migrations
{
    /// <inheritdoc />
    public partial class FKInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "WishList",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "WishList",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_MovieId",
                table: "WishList",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_UserID",
                table: "WishList",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_AppUser_UserID",
                table: "WishList",
                column: "UserID",
                principalTable: "AppUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_MediaContent_MovieId",
                table: "WishList",
                column: "MovieId",
                principalTable: "MediaContent",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishList_AppUser_UserID",
                table: "WishList");

            migrationBuilder.DropForeignKey(
                name: "FK_WishList_MediaContent_MovieId",
                table: "WishList");

            migrationBuilder.DropIndex(
                name: "IX_WishList_MovieId",
                table: "WishList");

            migrationBuilder.DropIndex(
                name: "IX_WishList_UserID",
                table: "WishList");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "WishList",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MovieId",
                table: "WishList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
