using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieCatalogBackend.Migrations
{
    public partial class Genres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreModel_Movies_MovieDbModelId",
                table: "GenreModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenreModel",
                table: "GenreModel");

            migrationBuilder.RenameTable(
                name: "GenreModel",
                newName: "Genres");

            migrationBuilder.RenameIndex(
                name: "IX_GenreModel_MovieDbModelId",
                table: "Genres",
                newName: "IX_Genres_MovieDbModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Movies_MovieDbModelId",
                table: "Genres",
                column: "MovieDbModelId",
                principalTable: "Movies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Movies_MovieDbModelId",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "GenreModel");

            migrationBuilder.RenameIndex(
                name: "IX_Genres_MovieDbModelId",
                table: "GenreModel",
                newName: "IX_GenreModel_MovieDbModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenreModel",
                table: "GenreModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreModel_Movies_MovieDbModelId",
                table: "GenreModel",
                column: "MovieDbModelId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
