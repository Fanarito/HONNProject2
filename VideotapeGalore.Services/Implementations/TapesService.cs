using VideotapeGalore.Models.Entities;
using VideotapeGalore.Repositories;
using VideotapeGalore.Services.Interfaces;

namespace VideotapeGalore.Services.Implementations
{
    public class TapesService : AbstractCrudService<Tape>, ITapesService
    {
        public TapesService(ApplicationContext context) : base(context)
        {
        }
    }
}