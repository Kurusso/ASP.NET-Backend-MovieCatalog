namespace MovieCatalogBackend.Models
{
    public class FavoriteMovies
    {
        public int Id { get; set; }
        public Guid MovieID { get; set; }
        public MovieDbModel Movie { get; set; }
        public Guid UserId { get;set; }
        public UserDbModel User { get; set; }
    }
}
