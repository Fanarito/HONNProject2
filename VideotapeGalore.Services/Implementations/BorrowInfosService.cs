using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Repositories;
using VideotapeGalore.Services.Filters;
using VideotapeGalore.Services.Interfaces;

namespace VideotapeGalore.Services.Implementations
{
    public class BorrowInfosService : IBorrowInfosService
    {
        private ApplicationContext Context { get; }

        public BorrowInfosService(ApplicationContext context)
        {
            Context = context;
        }
        
        public async Task<IEnumerable<Friend>> GetFriendsMatchingBorrowFilter(BorrowFilter borrowFilter)
        {
            return await GetMatchingBorrowFilter(borrowFilter)
                .Include(b => b.Friend)
                .Select(b => b.Friend)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<Tape>> GetTapesMatchingBorrowFilter(BorrowFilter borrowFilter)
        {
             return await GetMatchingBorrowFilter(borrowFilter)
                .Include(b => b.Tape)
                .Select(b => b.Tape)
                .Distinct()
                .ToListAsync();
        }
        
        private IQueryable<BorrowInfo> GetMatchingBorrowFilter(BorrowFilter borrowFilter)
        {
            IQueryable<BorrowInfo> query = Context.BorrowInfos;
            if (borrowFilter.LoanDate.HasValue)
            {
                var loanDate = borrowFilter.LoanDate.Value;
                if (borrowFilter.LoanDuration.HasValue)
                {
                    var loanDuration = borrowFilter.LoanDuration.Value;
                    var onLoanBy = loanDate.AddDays(-loanDuration);
                    query = query.Where(b => b.BorrowDate.Date <= onLoanBy
                                             && (b.ReturnDate == null || b.ReturnDate > loanDate));
                }
                else
                {
                    query = query.Where(b => b.BorrowDate.Date <= loanDate
                                             && (b.ReturnDate == null || b.ReturnDate > loanDate));
                }
            }

            return query;
        }
    }
}