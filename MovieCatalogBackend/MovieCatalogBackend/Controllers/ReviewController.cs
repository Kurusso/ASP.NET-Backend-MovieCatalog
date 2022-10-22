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
        public ReviewController(IReviewAddService reviewAdd)
        {
            _reviewAddService = reviewAdd;
        }
        [HttpPost("{movieId}/review/add")]
        [Authorize]
        public async Task<IActionResult> Post(Guid movieId, ReviewModifyModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _reviewAddService.AddReview(model, User.FindFirst("IdClaim").Value, movieId);
               return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
