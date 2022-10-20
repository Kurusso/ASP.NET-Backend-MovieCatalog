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
        private IFilmPageGetService _filmPageGetService;
        public MovieController(IFilmPageGetService filmPageGetService)
        {
            _filmPageGetService = filmPageGetService;
        }

        [HttpGet("{page}")]
        public async Task<IActionResult> GetPage(int page)
        {

            try
            {
                var PageModel = await _filmPageGetService.GetFilmsOnPage(page);

                return Ok(new
                {
                    Movies = PageModel.MovieElements,
                    PageInfo = PageModel.PageInfoModel
                }
            );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}

