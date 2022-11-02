using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogBackend.Models.DTO;
using MovieCatalogBackend.Services;

namespace MovieCatalogBackend.Controllers
{
    [Route("api/movie/")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IReviewAddService _reviewAddService;
        private IUserIdentityService _userIdentityService;

        public ReviewController(IReviewAddService reviewAdd, IUserIdentityService userIdentityService)
        {
            _reviewAddService = reviewAdd;
            _userIdentityService = userIdentityService;
        }
        [HttpPost("{movieId}/review/add")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post(Guid movieId, ReviewModifyModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string token = Request.Headers["Authorization"];
            bool check = await _userIdentityService.CheckJwtIsInBlackList(token);
            if (!check)
            {
                try
                {
                    await _reviewAddService.AddReview(model, User.FindFirst("IdClaim").Value, movieId);
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return Unauthorized("Jwt is in blacklist!");
            
        }
        [HttpPut("{movieId}/review/{id}/edit")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Put(Guid movieId, Guid id, ReviewModifyModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            string token = Request.Headers["Authorization"];
            bool check = await _userIdentityService.CheckJwtIsInBlackList(token);
            if (!check)
            {
                try
                {
                    await _reviewAddService.UpdateReview(model, User.FindFirst("IdClaim").Value, movieId, id);
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return Unauthorized("Jwt is in blacklist!");
            
        }
        [HttpDelete("{movieId}/review/{id}/delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(Guid movieId, Guid id)
        {
            string token = Request.Headers["Authorization"];
            bool check = await _userIdentityService.CheckJwtIsInBlackList(token);
            if (!check)
            {
                try
                {
                    await _reviewAddService.DeleteReview(User.FindFirst("IdClaim").Value, id, movieId);
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return Unauthorized("Jwt is in blacklist!");

            
        }
    }
}
