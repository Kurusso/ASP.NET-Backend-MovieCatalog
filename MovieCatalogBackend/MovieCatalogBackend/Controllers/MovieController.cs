using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogBackend.Models.DTO;
using MovieCatalogBackend.Services;

namespace MovieCatalogBackend.Controllers
{
    [Route("api/movies/")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private IMoviePageGetService _moviePageGetService;
        public MovieController(IMoviePageGetService moviePageGetService)
        {
            _moviePageGetService = moviePageGetService;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<MoviesPagedListModel>> GetPage(int page)
        {

            try
            {
                var PageModel = await _moviePageGetService.GetMoviesOnPage(page);

                return Ok(new MoviesPagedListModel
                {
                    Movies = PageModel.MovieElements,
                    PageInfo = PageModel.PageInfoModel
                }
            );
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
        [HttpGet("details/{id}")]
        public async Task<ActionResult<MovieDetailsModel>> GetMovieById(Guid id)
        {
            try
            {
                return Ok(await _moviePageGetService.GetMoviesById(id));
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            } 
        }
    }
}

