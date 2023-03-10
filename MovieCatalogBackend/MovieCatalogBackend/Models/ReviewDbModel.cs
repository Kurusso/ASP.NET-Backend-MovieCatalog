using MovieCatalogBackend.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace MovieCatalogBackend.Models
{
    public class ReviewDbModel
    {
        [Key]
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string? ReviewText { get; set; }
        public bool IsAnonymus { get; set; }
        public DateTime CreateDateTime { get; set; }
        public Guid AuthorId { get; set; }
        public Guid ReviewOnMovieID { get; set; }
        public UserDbModel Author { get; set; }
        public MovieDbModel ReviewOnMovie { get; set; }

    }
}
  