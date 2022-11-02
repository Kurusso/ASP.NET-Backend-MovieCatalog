using System.ComponentModel.DataAnnotations;

namespace MovieCatalogBackend.Models
{
    public class JwtDbModel
    {
        [Key]
        public string Jwt { get; set; }
    }
}
