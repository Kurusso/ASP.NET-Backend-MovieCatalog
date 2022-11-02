using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieCatalogBackend.Migrations
{
    public partial class ManyToManyGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Movies_MovieDbModelId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_MovieDbModelId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "MovieDbModelId",
                table: "Genres");

            migrationBuilder.CreateTable(
                name: "MoviesGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoviesGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviesGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviesGenres_GenreId",
                table: "MoviesGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesGenres_MovieId",
                table: "MoviesGenres",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviesGenres");

            migrationBuilder.AddColumn<Guid>(
                name: "MovieDbModelId",
                table: "Genres",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_MovieDbModelId",
                table: "Genres",
                column: "MovieDbModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Movies_MovieDbModelId",
                table: "Genres",
                column: "MovieDbModelId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
