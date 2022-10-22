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
    }
}
