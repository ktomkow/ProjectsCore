using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ProjectsCore.IdGeneration;
using ProjectsCore.Mongo.Interfaces;

namespace ProjectsCore.Mongo.Implementations.IdGeneration
{
    public class EntityIntIdGenerator : IEntityIdGenerator<int>
    {
        private const string CollectionName = "_identifiers";

        private static ConcurrentDictionary<string, SemaphoreSlim> semaphores = new ConcurrentDictionary<string, SemaphoreSlim>();

        private readonly IMongoDatabase db;

        public EntityIntIdGenerator(IDbFactory dbFactory)
        {
            db = dbFactory.Create();
        }

        public async Task<int> Generate(Type type)
        {
            var collection = GetCollection();
            string typeName = type.Name;
            var filter = Builders<IntIdState>.Filter.Eq("_id", typeName);

            var semaphore = semaphores.GetOrAdd(typeName, (_) => new SemaphoreSlim(1, 1));
            await semaphore.WaitAsync();

            List<IntIdState> entities = await (await collection.FindAsync(filter).ConfigureAwait(false)).ToListAsync().ConfigureAwait(false);
            IntIdState idState;

            if (entities.Any() == false)
            {
                idState = new IntIdState(typeName);
                await collection.InsertOneAsync(idState).ConfigureAwait(false);

            }
            else if (entities.Count > 1)
            {
                semaphore.Release();
                throw new Exception($"WTF, more than 1 Id container for type: [{typeName}] ");
            }
            else
            {
                idState = entities.First();
            }

            int result = idState.Tick();

            await collection.FindOneAndReplaceAsync(filter, idState).ConfigureAwait(false);

            semaphore.Release();

            return result;
        }

        private IMongoCollection<IntIdState> GetCollection()
        {
            var collection = db.GetCollection<IntIdState>(CollectionName);

            return collection;
        }

        private class IntIdState : IdState<int>
        {
            public IntIdState(string typeName) : base(typeName) { }

            public override int Tick()
            {
                return ++Value;
            }
        }
    }
}
