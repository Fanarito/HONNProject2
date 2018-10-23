using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Repositories;
using VideotapeGalore.Services.Filters;
using VideotapeGalore.Services.Interfaces;

namespace VideotapeGalore.Services.Implementations
{
    public class FriendsService : AbstractCrudService<Friend>, IFriendsService
    {
        public FriendsService(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Tape>> TapesOnLoan(int friendId)
        {
            return await Context.Set<BorrowInfo>()
                .Include(b => b.Tape)
                .Where(b => b.FriendId == friendId && !b.ReturnDate.HasValue)
                .Select(b => b.Tape).ToListAsync();
        }

        public async Task RegisterTape(int friendId, int tapeId)
        {
            if (await Context.Set<BorrowInfo>().AnyAsync(b => b.TapeId == tapeId && b.ReturnDate == null))
            {
                throw new Exception();
            }

            Context.Add(new BorrowInfo
            {
                FriendId = friendId,
                TapeId = tapeId
            });
            Context.SaveChanges();
        }

        public async Task ReturnTape(int friendId, int tapeId)
        {
            var burrowInfo = await Context.Set<BorrowInfo>()
                .Where(b => b.FriendId == friendId && b.TapeId == tapeId && b.ReturnDate == null)
                .SingleAsync();
            burrowInfo.ReturnDate = DateTime.Now;
            Context.Update(burrowInfo);
            Context.SaveChanges();
        }
    }
}