using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieCatalogBackend.Migrations
{
    public partial class setNewForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movies_ReviewOnMovieId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "ReviewOnMovieId",
                table: "Reviews",
                newName: "ReviewOnMovieID");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ReviewOnMovieId",
                table: "Reviews",
                newName: "IX_Reviews_ReviewOnMovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movies_ReviewOnMovieID",
                table: "Reviews",
                column: "ReviewOnMovieID",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movies_ReviewOnMovieID",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "ReviewOnMovieID",
                table: "Reviews",
                newName: "ReviewOnMovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ReviewOnMovieID",
                table: "Reviews",
                newName: "IX_Reviews_ReviewOnMovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movies_ReviewOnMovieId",
                table: "Reviews",
                column: "ReviewOnMovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
