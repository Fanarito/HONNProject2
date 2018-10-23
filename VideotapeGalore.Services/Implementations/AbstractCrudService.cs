using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideotapeGalore.Repositories;
using VideotapeGalore.Services.Exceptions;
using VideotapeGalore.Services.Interfaces;

namespace VideotapeGalore.Services.Implementations
{
    public abstract class AbstractCrudService<T> : ICrudService<T> where T : class
    {
        protected ApplicationContext Context { get; set; }

        protected AbstractCrudService(ApplicationContext context)
        {
            Context = context;
        }
        
        public void Create(T t)
        {
            Context.Add(t);
            Context.SaveChanges();
        }

        public void Delete(T t)
        {
            Context.Remove(t);
            Context.SaveChanges();
        }

        public void Update(T t)
        {
            Context.Update(t);
            Context.SaveChanges();
        }

        public async Task<T> GetSingle(params object[] id)
        {
            var res = await Context.FindAsync<T>(id);
            if (res == null)
            {
                throw new NotFoundException($"{typeof(T).Name} not found");
            }
            return res;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }
    }
}