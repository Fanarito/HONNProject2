using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Repositories;
using VideotapeGalore.Services.Interfaces;

namespace VideotapeGalore.Services.Implementations
{
    public class RecommendationService : IRecommendationService
    {
        private ApplicationContext Context { get; }

        public RecommendationService(ApplicationContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<Tape>> GetRecommendations(int friendId)
        {
            return await (
                    from t in Context.Tapes
                    where t.BorrowInfos.All(bi => bi.FriendId != friendId)
                    orderby t.Reviews.Average(r => (decimal?)r.Rating) descending
                    select t
            ).Take(10).ToListAsync();
        }
    }
}
