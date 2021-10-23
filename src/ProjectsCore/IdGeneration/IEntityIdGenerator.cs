using System;
using System.Threading.Tasks;

namespace ProjectsCore.IdGeneration
{
    public interface IEntityIdGenerator<TKey>
        where TKey : struct
    {
        Task<TKey> Generate(Type type);
    }
}