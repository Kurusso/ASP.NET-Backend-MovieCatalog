using MovieCatalogBackend.Migrations;
using MovieCatalogBackend.Models.DTO;

namespace MovieCatalogBackend.Models
{
    public class MovieGenreDbModel
    {
        public int Id { get; set; }
        public Guid GenreId { get; set; }
        public Guid MovieId { get; set; }
        public GenreDbModel Genre { get; set; }
        public MovieDbModel Movie { get; set; }
    }
}
