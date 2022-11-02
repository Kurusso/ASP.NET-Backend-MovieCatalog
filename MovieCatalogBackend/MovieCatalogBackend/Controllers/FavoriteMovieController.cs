using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogBackend.Models.DTO;
using MovieCatalogBackend.Services;

namespace MovieCatalogBackend.Controllers
{
    [Route("/api/favorites")]
    [ApiController]
    public class FavoriteMovieController : ControllerBase
    {
        private IUserIdentityService _userIdentityService;
        private IFavoriteMovieService _favoriteMovieService;
        public FavoriteMovieController(IUserIdentityService userIdentityService, IFavoriteMovieService favoriteMovieService)
        {
            _userIdentityService = userIdentityService;
            _favoriteMovieService = favoriteMovieService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<MoviesListModel>> Get()
        {
            string token = Request.Headers["Authorization"];
            bool check = await _userIdentityService.CheckJwtIsInBlackList(token);
            if (!check)
            {
                try
                {
                    return Ok(new MoviesListModel { Movies = await _favoriteMovieService.GetFavoriteMovies(new Guid(User.FindFirst("IdClaim").Value)) });
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return Unauthorized("Jwt is in blacklist!");
        }
        [HttpPost("{id}/add")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post(Guid id)
        {
            string token = Request.Headers["Authorization"];
            bool check = await _userIdentityService.CheckJwtIsInBlackList(token);
            if (!check)
            {
                try
                {
                    await _favoriteMovieService.AddMovieToFavorite(id, new Guid(User.FindFirst("IdClaim").Value));
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return Unauthorized("Jwt is in blacklist!");
        }
        [HttpDelete("{id}/delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(Guid id)
        {
            string token = Request.Headers["Authorization"];
            bool check = await _userIdentityService.CheckJwtIsInBlackList(token);
            if (!check)
            {
                try
                {
                    await _favoriteMovieService.DeleteMovieFromFavorite(id, new Guid(User.FindFirst("IdClaim").Value));
                    return Ok();
                }
                catch(Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return Unauthorized("Jwt is in blacklist!");
        }
    }
}
