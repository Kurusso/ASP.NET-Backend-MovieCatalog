using System.ComponentModel.DataAnnotations;

namespace MovieCatalogBackend.Models.DTO
{
    public class LoginCredentials
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
