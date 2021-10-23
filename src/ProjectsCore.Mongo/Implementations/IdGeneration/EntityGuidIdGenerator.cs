using ProjectsCore.IdGeneration;
using System;
using System.Threading.Tasks;

namespace MongoPack.IdGeneration
{
    public class EntityGuidIdGenerator : IEntityIdGenerator<Guid>
    {
        public async Task<Guid> Generate(Type type)
        {
            Guid guid = Guid.NewGuid();

            return await Task.FromResult(guid);
        }
    }
}
