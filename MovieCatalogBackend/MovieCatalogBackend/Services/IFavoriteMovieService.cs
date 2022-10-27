using MovieCatalogBackend.Models.DTO;

namespace MovieCatalogBackend.Services
{
    public interface IFavoriteMovieService
    {
        public Task AddMovieToFavorite(Guid movieId, Guid userId);
        public Task DeleteMovieFromFavorite(Guid movieId, Guid userId);
        public Task<List<MovieElementModel>> GetFavoriteMovies(Guid userId);
    }
}
