using ProjectsCore.Models;
using System.Threading.Tasks;

namespace ProjectsCore.Persistence
{
    public interface IRepository<TKey, TEntity> 
        where TKey : struct
        where TEntity : IEntity<TKey>
    {
        Task<TEntity> Get(TKey key);

        Task<TEntity> GetAll();

        Task<TEntity> Insert(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> Remove(TEntity entity);
    }
}
