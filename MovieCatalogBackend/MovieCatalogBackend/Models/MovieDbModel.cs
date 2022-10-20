using MovieCatalogBackend.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace MovieCatalogBackend.Models
{
    public class MovieDbModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Poster { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public List<GenreModel> Genres { get; set; }
    }
}
