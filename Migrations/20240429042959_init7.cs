using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication6.Migrations
{
    /// <inheritdoc />
    public partial class init7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Blogs_BlogID1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_BlogID1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "BlogID1",
                table: "Comments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogID1",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogID1",
                table: "Comments",
                column: "BlogID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Blogs_BlogID1",
                table: "Comments",
                column: "BlogID1",
                principalTable: "Blogs",
                principalColumn: "BlogID");
        }
    }
}
