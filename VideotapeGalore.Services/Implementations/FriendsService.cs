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
    }
}