using Microsoft.EntityFrameworkCore;
using MovieCatalogBackend.Models;

namespace MovieCatalogBackend.Context
{
    public class MovieCatalogDbContext: DbContext
    {
        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<MovieDbModel> Movies { get; set; }
        public DbSet<ReviewDbModel> Reviews { get; set; }
        public MovieCatalogDbContext(DbContextOptions<MovieCatalogDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
