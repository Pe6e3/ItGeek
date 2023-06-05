﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItGeek.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostContents_Posts_PostId",
                table: "PostContents");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "PostContents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostContents_Posts_PostId",
                table: "PostContents",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostContents_Posts_PostId",
                table: "PostContents");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "PostContents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PostContents_Posts_PostId",
                table: "PostContents",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
