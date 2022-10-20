using Microsoft.AspNetCore.Mvc;
using MovieCatalogBackend.Context;
using MovieCatalogBackend.Migrations;
using MovieCatalogBackend.Models;
using MovieCatalogBackend.Models.DTO;

namespace MovieCatalogBackend.Services
{
    public class FilmPageGetService : IFilmPageGetService
    {
        private MovieCatalogDbContext _context;
        private  List<MovieDbModel> Movies;
        private int _pageCount;
        private List<MovieElementModel> MoviesElements;
        private int _pageSize = 10;
        public FilmPageGetService(MovieCatalogDbContext context)
        {
            _context = context;
        }
         
        public async Task<PageModel> GetFilmsOnPage(int page)
        {
            if ((_context.Movies.Count() % _pageSize) == 0)
            {
                _pageCount = (_context.Movies.Count() / _pageSize);
            }
            else
            {
                _pageCount = (_context.Movies.Count() / _pageSize) + 1; 
            }
            if(page > _pageCount || page<=0)
            {
                throw new Exception("Incorrect page number");
            }

            Movies = _context.Movies.Skip(_pageSize*(page-1)).Take(_pageSize).ToList();


            MoviesElements = Movies.Select(x => new MovieElementModel() { Country=x.Country, Genres=x.Genres, Id=x.Id, Name=x.Name, Poster=x.Poster, Year=x.Year} ).ToList();

            return new PageModel { MovieElements=MoviesElements, PageInfoModel=new PageInfoModel { CurrentPage=page, PageCount=_pageCount, PageSize=MoviesElements.Count()} };
        }

        public async Task<MovieDetailsModel> GetFilmById(Guid id)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            if(movie == null)
            {
                throw new Exception("Film with this id doesn't exists");
            }
            return new MovieDetailsModel
            {
                AgeLimit = movie.AgeLimit,
                Budget = movie.Budget,
                Country = movie.Country,
                Genres = movie.Genres,
                Id = movie.Id,
                Name = movie.Name,
                Poster = movie.Poster,
                Year = movie.Year,
                Tagline = movie.Tagline,
                Time = movie.Time,
                Description = movie.Description,
                Director = movie.Director,
                Fees = movie.Fees
            };
            
        }
    }
}


