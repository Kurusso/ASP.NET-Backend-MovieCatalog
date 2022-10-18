using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieCatalogBackend.Models
{
    public class UserRegisterModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(3)]
        [MaxLength(20)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender? Gender { get; set; }

    }
}
