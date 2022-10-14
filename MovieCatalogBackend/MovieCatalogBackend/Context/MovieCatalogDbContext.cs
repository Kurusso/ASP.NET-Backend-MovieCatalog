using Microsoft.EntityFrameworkCore;

namespace MovieCatalogBackend.Context
{
    public class MovieCatalogDbContext: DbContext
    {
        public MovieCatalogDbContext(DbContextOptions<MovieCatalogDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
