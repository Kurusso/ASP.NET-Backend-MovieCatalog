using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MovieCatalogBackend.Models
{
    public class UserDbModel
    {
        [Key]
       public int Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public string? Avatar { get; set; }
        public ICollection<ReviewDbModel>? Reviews { get; set; }

        public Gender? Gender { get; set; }
    }
}
