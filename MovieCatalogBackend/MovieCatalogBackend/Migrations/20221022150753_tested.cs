using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieCatalogBackend.Migrations
{
    public partial class tested : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movies_MovieDbModelId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_MovieDbModelId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "MovieDbModelId",
                table: "Reviews");

            migrationBuilder.AddColumn<Guid>(
                name: "ReviewOnMovieId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewOnMovieId",
                table: "Reviews",
                column: "ReviewOnMovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movies_ReviewOnMovieId",
                table: "Reviews",
                column: "ReviewOnMovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movies_ReviewOnMovieId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReviewOnMovieId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewOnMovieId",
                table: "Reviews");

            migrationBuilder.AddColumn<Guid>(
                name: "MovieDbModelId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieDbModelId",
                table: "Reviews",
                column: "MovieDbModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movies_MovieDbModelId",
                table: "Reviews",
                column: "MovieDbModelId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
