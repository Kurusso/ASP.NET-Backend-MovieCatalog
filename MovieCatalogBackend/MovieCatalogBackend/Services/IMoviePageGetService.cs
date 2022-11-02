using Microsoft.AspNetCore.Mvc;
using MovieCatalogBackend.Models;
using MovieCatalogBackend.Models.DTO;

namespace MovieCatalogBackend.Services
{
    public interface IMoviePageGetService
    {
        public Task<PageModel> GetMoviesOnPage(int page);
        public Task<MovieDetailsModel> GetMoviesById(Guid id);
    }
}
