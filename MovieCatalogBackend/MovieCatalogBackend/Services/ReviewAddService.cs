using MovieCatalogBackend.Context;
using MovieCatalogBackend.Migrations;
using MovieCatalogBackend.Models;
using MovieCatalogBackend.Models.DTO;

namespace MovieCatalogBackend.Services
{
    public class ReviewAddService : IReviewAddService
    {
        private MovieCatalogDbContext _context;
        public ReviewAddService(MovieCatalogDbContext context)
        {
            _context = context;
        }
        public async Task AddReview(ReviewModifyModel model, string userId, Guid MovieId)
        {
            var User = _context.Users.Find(int.Parse(userId));
            var Movie = _context.Movies.Find(MovieId);
            if(User == null)
            {
                throw new Exception("Incorrect User Id");
            }
            if(Movie == null)
            {
                throw new Exception("Incorrect Movie Id");
            }
            var reviewDbModel = new ReviewDbModel { Author = User, IsAnonymus = model.IsAnonymus, CreateDateTime = DateTime.Now, Rating = model.Rating, ReviewText = model.ReviewText, ReviewOnMovie = Movie, Id = Guid.NewGuid() };
            await _context.Reviews.AddAsync(reviewDbModel);
            Movie.Reviews.Add(reviewDbModel);
            User.Reviews.Add(reviewDbModel);           
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReview(string userId, Guid reviewId, Guid movieId)
        {
            var review = _context.Reviews.Find(reviewId);
            if (review == null)
            {
                throw new Exception("Review with this Id doesn't exists");
            }
            if (review.AuthorId != int.Parse(userId))
            {
                throw new Exception("You can't delete review of other User");
            }
            if (review.ReviewOnMovieID != movieId)
            {
                throw new Exception("It is review to another movie!");
            }
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReview(ReviewModifyModel model, string userId, Guid MovieId, Guid reviewId)
        {
            var review = _context.Reviews.Find(reviewId);
            if(review == null)
            {
                throw new Exception("Review with this Id doesn't exists");
            }
            if (review.AuthorId != int.Parse(userId))
            {
                throw new Exception("You can't edit review of other User");
            }
            if (review.ReviewOnMovieID != MovieId)
            {
                throw new Exception("It is review to another movie!");
            }
            review.ReviewText = model.ReviewText;
            review.Rating=model.Rating;
            review.IsAnonymus = model.IsAnonymus;
            await _context.SaveChangesAsync();
        }

    }
}
