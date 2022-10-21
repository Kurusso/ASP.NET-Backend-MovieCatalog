using Microsoft.AspNetCore.Mvc;
using MovieCatalogBackend.Migrations;
using MovieCatalogBackend.Models;
using MovieCatalogBackend.Models.DTO;

namespace MovieCatalogBackend.Services
{
    public interface IFilmPageGetService
    {
        public Task<PageModel> GetFilmsOnPage(int page);
        public Task<MovieDetailsModel> GetFilmById(Guid id);
    }
}
