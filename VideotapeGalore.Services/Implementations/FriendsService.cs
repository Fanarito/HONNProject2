using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Repositories;
using VideotapeGalore.Services.Interfaces;

namespace VideotapeGalore.Services.Implementations
{
    public class FriendsService : AbstractCrudService<Friend>, IFriendsService
    {
        public FriendsService(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Tape>>TapesOnLoan(int friendId)
        {
            return await Context.Set<Tape>()
                .Include(b => b.BorrowInfos)
                .ThenInclude(f => f.FriendId)
                .Where(t => t.BorrowInfos.Any(b => b.FriendId == friendId)).ToListAsync();
        }
        
        
    }
}