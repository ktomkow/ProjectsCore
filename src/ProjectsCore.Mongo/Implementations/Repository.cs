using MongoDB.Driver;
using ProjectsCore.IdGeneration;
using ProjectsCore.Models;
using ProjectsCore.Mongo.Extensions;
using ProjectsCore.Mongo.Interfaces;
using ProjectsCore.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsCore.Mongo.Implementations
{
    public class Repository<TKey, TEntity> : IRepository<TKey, TEntity>
        where TKey : struct
        where TEntity : Entity<TKey>
    {
        private readonly ICollectionNameResolver resolver;
        private readonly IEntityIdGenerator<TKey> idGenerator;
        private readonly IMongoDatabase db;

        public Repository(
            IDbFactory dbFactory,
            ICollectionNameResolver resolver,
            IEntityIdGenerator<TKey> idGenerator)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            this.idGenerator = idGenerator;
            db = dbFactory.Create();
        }

        public Task<TEntity> Get(TKey key)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<TEntity>> GetAll()
        {
            var result = GetCollection().AsQueryable();

            return await Task.FromResult(result.AsQueryable());
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            var collection = GetCollection();

            TKey id = await idGenerator.Generate(typeof(TEntity));
            entity.SetId(id);

            await collection.InsertOneAsync(entity);

            return entity;
        }

        public Task<TEntity> Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IMongoCollection<TEntity> GetCollection()
        {
            string collectionName = resolver.Resolve(typeof(TEntity));
            var collection = db.GetCollection<TEntity>(collectionName);

            return collection;
        }
    }
}
