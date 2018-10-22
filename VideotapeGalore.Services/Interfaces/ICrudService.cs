using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideotapeGalore.Services.Interfaces
{
    public interface ICrudService<T> where T : class
    {
        void Create(T t);
        void Delete(T t);
        void Update(T t);
        Task<T> GetSingle(object id);
        Task<IEnumerable<T>> GetAll();
    }
}