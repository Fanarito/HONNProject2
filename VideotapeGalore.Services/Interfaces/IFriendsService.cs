using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using VideotapeGalore.Models.Entities;

namespace VideotapeGalore.Services.Interfaces
{
    public interface IFriendsService : ICrudService<Friend>
    {
        Task<IEnumerable<Tape>> TapesOnLoan(int friendId);
    }
}