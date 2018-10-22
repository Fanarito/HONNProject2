using VideotapeGalore.Models.Entities;
using VideotapeGalore.Repositories;
using VideotapeGalore.Services.Interfaces;

namespace VideotapeGalore.Services.Implementations
{
    public class ReviewsService : AbstractCrudService<Review>, IReviewsService
    {
        public ReviewsService(ApplicationContext context) : base(context)
        {
        }
    }
}