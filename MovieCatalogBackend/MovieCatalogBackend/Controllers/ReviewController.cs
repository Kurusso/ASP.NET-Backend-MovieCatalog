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
        [HttpPut("{movieId}/review/{id}/edit")]
        [Authorize]
        public async Task<IActionResult> Put(Guid movieId, Guid id, ReviewModifyModel model)
        {
            try
            {
               await _reviewAddService.UpdateReview(model, User.FindFirst("IdClaim").Value, movieId, id);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{movieId}/review/{id}/delete")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid movieId, Guid id)
        {
            try
            {
                await _reviewAddService.DeleteReview(User.FindFirst("IdClaim").Value, id, movieId);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
