using System.ComponentModel.DataAnnotations;

namespace MovieCatalogBackend.Models.DTO
{
    public class MovieDetailsModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Poster { get; set; }

        [Required]
        public int Year { get; set; }

        public string? Country { get; set; }

        [Required]
        public ICollection<GenreModel> Genres { get; set; }

        public ICollection<ReviewModel>? Reviews { get; set; }

        public int Time { get; set; }

        public string? Tagline { get; set; }

        public string? Description { get; set; }

        public string? Director { get; set; }

        public int? Budget { get; set; }

        public int? Fees { get; set; }

        public int? AgeLimit { get; set; }
    }
}
