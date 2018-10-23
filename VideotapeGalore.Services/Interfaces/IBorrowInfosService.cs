using System.Collections.Generic;
using System.Threading.Tasks;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Services.Filters;

namespace VideotapeGalore.Services.Interfaces
{
    public interface IBorrowInfosService
    {
        Task<IEnumerable<Friend>> GetFriendsMatchingBorrowFilter(BorrowFilter borrowFilter);
        Task<IEnumerable<Tape>> GetTapesMatchingBorrowFilter(BorrowFilter borrowFilter);
    }
}