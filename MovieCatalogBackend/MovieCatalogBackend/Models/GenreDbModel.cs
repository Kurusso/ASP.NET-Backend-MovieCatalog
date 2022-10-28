namespace MovieCatalogBackend.Models
{
    public class GenreDbModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieGenreDbModel> Movies { get; set; }
    }
}
