using ProjectsCore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsCore.Persistence
{
    public interface IRepository<TKey, TEntity> 
        where TKey : struct
        where TEntity : IEntity<TKey>
    {
        Task<TEntity> Get(TKey key);

        Task<IQueryable<TEntity>> GetAll();

        Task<TEntity> Insert(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> Remove(TEntity entity);
    }
}
