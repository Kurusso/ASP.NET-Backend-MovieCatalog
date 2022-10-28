using Microsoft.EntityFrameworkCore;
using MovieCatalogBackend.Models;
using MovieCatalogBackend.Models.DTO;

namespace MovieCatalogBackend.Context
{
    public class MovieCatalogDbContext: DbContext
    {
        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<MovieDbModel> Movies { get; set; }
        public DbSet<ReviewDbModel> Reviews { get; set; }
        public DbSet<JwtDbModel> Jwt { get; set; }
        public DbSet<FavoriteMovies> FavoriteMovies { get; set; }
        public DbSet<GenreModel> Genres { get; set; }
        public MovieCatalogDbContext(DbContextOptions<MovieCatalogDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
