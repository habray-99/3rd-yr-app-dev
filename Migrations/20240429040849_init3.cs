using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication6.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMetric_Users_UserID",
                table: "UserMetric");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_UserID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "CustomUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomUser",
                table: "CustomUser",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomUser_AspNetUsers_UserID",
                table: "CustomUser",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMetric_CustomUser_UserID",
                table: "UserMetric",
                column: "UserID",
                principalTable: "CustomUser",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomUser_AspNetUsers_UserID",
                table: "CustomUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMetric_CustomUser_UserID",
                table: "UserMetric");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomUser",
                table: "CustomUser");

            migrationBuilder.RenameTable(
                name: "CustomUser",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMetric_Users_UserID",
                table: "UserMetric",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_UserID",
                table: "Users",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
