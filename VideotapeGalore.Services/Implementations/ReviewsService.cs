using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Review>> GetAllForTape(int tapeId)
        {
            return await Context.Reviews.Where(r => r.TapeId == tapeId).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAllForFriend(int friendId)
        {
            return await Context.Reviews.Where(r => r.FriendId == friendId).ToListAsync();
        }

        public async Task<Review> GetFriendReviewOfTape(int friendId, int tapeId)
        {
            return await GetSingle(friendId, tapeId);
        }
    }
}