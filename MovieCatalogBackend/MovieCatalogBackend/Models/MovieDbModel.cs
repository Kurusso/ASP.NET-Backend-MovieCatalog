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
        public ICollection<GenreModel> Genres { get; set; }

        public ICollection<ReviewDbModel> Reviews { get; set; }

        public int Time { get; set; }

        public string? Tagline { get; set; }

        public string? Description { get; set; }

        public string? Director { get; set; }

        public int? Budget { get; set; }

        public int? Fees { get; set; }

        public int? AgeLimit { get; set; }
    }
}
