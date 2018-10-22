using System.Collections.Generic;
using System.Threading.Tasks;
using VideotapeGalore.Models.Entities;

namespace VideotapeGalore.Services.Interfaces
{
    public interface IReviewsService : ICrudService<Review>
    {
        Task<IEnumerable<Review>> GetAllForTape(int tapeId);
        Task<IEnumerable<Review>> GetAllForFriend(int friendId);
        Task<Review> GetFriendReviewOfTape(int friendId, int tapeId);
    }
}