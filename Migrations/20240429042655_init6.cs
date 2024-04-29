using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication6.Migrations
{
    /// <inheritdoc />
    public partial class init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReactions_Comments_CommentID1",
                table: "CommentReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentReactions_ReactionTypes_ReactionTypeID1",
                table: "CommentReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_ReactionTypes_ReactionTypeID1",
                table: "Reactions");

            migrationBuilder.DropIndex(
                name: "IX_Reactions_ReactionTypeID1",
                table: "Reactions");

            migrationBuilder.DropIndex(
                name: "IX_CommentReactions_CommentID1",
                table: "CommentReactions");

            migrationBuilder.DropIndex(
                name: "IX_CommentReactions_ReactionTypeID1",
                table: "CommentReactions");

            migrationBuilder.DropColumn(
                name: "ReactionTypeID1",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "CommentID1",
                table: "CommentReactions");

            migrationBuilder.DropColumn(
                name: "ReactionTypeID1",
                table: "CommentReactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReactionTypeID1",
                table: "Reactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentID1",
                table: "CommentReactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReactionTypeID1",
                table: "CommentReactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_ReactionTypeID1",
                table: "Reactions",
                column: "ReactionTypeID1");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReactions_CommentID1",
                table: "CommentReactions",
                column: "CommentID1");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReactions_ReactionTypeID1",
                table: "CommentReactions",
                column: "ReactionTypeID1");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReactions_Comments_CommentID1",
                table: "CommentReactions",
                column: "CommentID1",
                principalTable: "Comments",
                principalColumn: "CommentID");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReactions_ReactionTypes_ReactionTypeID1",
                table: "CommentReactions",
                column: "ReactionTypeID1",
                principalTable: "ReactionTypes",
                principalColumn: "ReactionTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_ReactionTypes_ReactionTypeID1",
                table: "Reactions",
                column: "ReactionTypeID1",
                principalTable: "ReactionTypes",
                principalColumn: "ReactionTypeID");
        }
    }
}
