using VideotapeGalore.Models.Entities;
using VideotapeGalore.Repositories;
using VideotapeGalore.Services.Implementations;

namespace VideotapeGalore.Tests.Services
{
    public class CrudServiceImpl : AbstractCrudService<Tape>
    {
        public CrudServiceImpl(ApplicationContext context) : base(context)
        {
        }
    }
}