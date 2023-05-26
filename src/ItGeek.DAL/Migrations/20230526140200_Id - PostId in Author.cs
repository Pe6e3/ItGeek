using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItGeek.DAL.Migrations
{
    /// <inheritdoc />
    public partial class IdPostIdinAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostAuthors_Posts_PostId",
                table: "PostAuthors");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PostAuthors");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "PostAuthors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostAuthors_Posts_PostId",
                table: "PostAuthors",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostAuthors_Posts_PostId",
                table: "PostAuthors");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "PostAuthors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PostAuthors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PostAuthors_Posts_PostId",
                table: "PostAuthors",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
