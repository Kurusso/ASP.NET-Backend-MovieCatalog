using MovieCatalogBackend.Models.DTO;

namespace MovieCatalogBackend.Services
{
    public interface IReviewAddService
    {
        public Task AddReview(ReviewModifyModel model, string userId, Guid MovieId);
    }
}
