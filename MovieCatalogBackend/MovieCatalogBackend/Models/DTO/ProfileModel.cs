using System.ComponentModel.DataAnnotations;

namespace MovieCatalogBackend.Models.DTO
{
    public class ProfileModel
    {
        public Guid Id { get; set; }

        public string NickName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string? AvatarLink { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender? Gender { get; set; }
    }
}
