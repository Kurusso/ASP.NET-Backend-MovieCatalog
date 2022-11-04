using MovieCatalogBackend.Context;
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
            var User = _context.Users.Find(Guid.Parse(userId));
            var Movie = _context.Movies.Find(MovieId);
            var currentReview = _context.Reviews.FirstOrDefault(x => x.AuthorId ==  Guid.Parse(userId) && x.ReviewOnMovieID==MovieId);
            if (currentReview != null)
            {
                throw new InvalidOperationException("You already have a review on this movie!");
            }
            if(User == null)
            {
                throw new ArgumentException("Incorrect User Id");
            }
            if(Movie == null)
            {
                throw new ArgumentException("Incorrect Movie Id");
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
                throw new Exception("Review with this Id doesn't exists!");
            }
            if (review.AuthorId != new Guid(userId))
            {
                throw new Exception("You haven't got review with this id!");
            }
            if (review.ReviewOnMovieID != movieId)
            {
                throw new Exception("You haven't got review on this movie!");
            }
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReview(ReviewModifyModel model, string userId, Guid MovieId, Guid reviewId)
        {
            var review = _context.Reviews.Find(reviewId);
            if(review == null)
            {
                throw new Exception("Review with this Id doesn't exists!");
            }
            if (review.AuthorId != new Guid(userId))
            {
                throw new Exception("You haven't got review with this id!");
            }
            if (review.ReviewOnMovieID != MovieId)
            {
                throw new Exception("You haven't got review on this movie!");
            }
            review.ReviewText = model.ReviewText;
            review.Rating=model.Rating;
            review.IsAnonymus = model.IsAnonymus;
            await _context.SaveChangesAsync();
        }

    }
}
