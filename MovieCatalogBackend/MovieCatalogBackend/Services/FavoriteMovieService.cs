using Microsoft.EntityFrameworkCore;
using MovieCatalogBackend.Context;
using MovieCatalogBackend.Models;
using MovieCatalogBackend.Models.DTO;

namespace MovieCatalogBackend.Services
{
    public class FavoriteMovieService : IFavoriteMovieService
    {
        private MovieCatalogDbContext _context;
        public FavoriteMovieService(MovieCatalogDbContext context)
        {
            _context = context;
        }

        public async Task AddMovieToFavorite(Guid movieId, Guid userId)
        {
            var movie = _context.FavoriteMovies.FirstOrDefault(x => x.MovieID == movieId && x.UserId == userId);
            if (movie != null)
            {
                throw new InvalidOperationException("You already have this movie in your Favourites!");
            }
            if (_context.Users.FirstOrDefault(u => u.Id == userId) == null)
            {
                throw new ArgumentException("This user doesn't exist!");
            }
            if (_context.Movies.FirstOrDefault(x => x.Id == movieId) == null)
            {
                throw new ArgumentException("This movie doesn't exist!");
            }
            _context.FavoriteMovies.Add(new FavoriteMovies { MovieID = movieId, UserId = userId });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieFromFavorite(Guid movieId, Guid userId)
        {
            var favoriteMovie = _context.FavoriteMovies.FirstOrDefault(x => x.MovieID == movieId && x.UserId == userId);
            if (favoriteMovie == null)
            {
                throw new Exception("You haven't got this movie in favorites!");
            }
            _context.FavoriteMovies.Remove(favoriteMovie);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MovieElementModel>> GetFavoriteMovies(Guid userId)
        {
            var movies = _context.FavoriteMovies.Include(x => x.Movie).ThenInclude(t => t.Genres).Where(u => u.UserId == userId).Select(x => new MovieElementModel()
            {
                Country = x.Movie.Country,
                Genres = _context.MoviesGenres.Include(s => s.Genre).Where(s => s.MovieId == x.Movie.Id).Select(u => new GenreModel { Id = u.Genre.Id, Name = u.Genre.Name }).ToList(),
                Id = x.Movie.Id,
                Name = x.Movie.Name,
                Poster = x.Movie.Poster,
                Year = x.Movie.Year,
                Reviews = _context.Reviews.Where(r => r.ReviewOnMovieID == x.Movie.Id).Select(u => new ReviewShortModel { Id = u.Id, Rating = u.Rating }).ToList()
            }).ToList();
            return movies;
        }
    }
}