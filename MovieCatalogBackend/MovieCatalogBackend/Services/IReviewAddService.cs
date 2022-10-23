using MovieCatalogBackend.Models.DTO;

namespace MovieCatalogBackend.Services
{
    public interface IReviewAddService
    {
        public Task AddReview(ReviewModifyModel model, string userId, Guid MovieId);
        public Task UpdateReview(ReviewModifyModel model, string userId, Guid MovieId, Guid reviewId);
        public Task DeleteReview(string userId, Guid reviewId, Guid movieId);
    }
}
