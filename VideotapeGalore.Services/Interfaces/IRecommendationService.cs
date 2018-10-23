using System.Collections.Generic;
using System.Threading.Tasks;
using VideotapeGalore.Models.Entities;

namespace VideotapeGalore.Services.Interfaces
{
    public interface IRecommendationService
    {
        Task<IEnumerable<Tape>> GetRecommendations(int friendId);
    }
}