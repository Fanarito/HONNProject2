using System.Collections.Generic;
using System.Threading.Tasks;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Services.Filters;

namespace VideotapeGalore.Services.Interfaces
{
    public interface IFriendsService : ICrudService<Friend>
    {
        Task<IEnumerable<Tape>> TapesOnLoan(int friendId);
        Task RegisterTape(int friendId, int tapeId);
        Task ReturnTape(int friendId, int tapeId);
    }
}