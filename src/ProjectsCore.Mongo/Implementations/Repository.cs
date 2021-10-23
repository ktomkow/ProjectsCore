using MongoDB.Bson;
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

        public async Task<TEntity> Get(TKey key)
        {
            return (await this.GetAll()).FirstOrDefault(x => x.Id.Equals(key));
        }

        public async Task<IQueryable<TEntity>> GetAll()
        {
            var result = this.GetCollection().AsQueryable();

            return await Task.FromResult(result.AsQueryable());
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            var collection = this.GetCollection();

            TKey id = await idGenerator.Generate(typeof(TEntity));
            entity.SetId(id);

            await collection.InsertOneAsync(entity);

            return entity;
        }

        public Task<TEntity> Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var collection = this.GetCollection();
            var builder = Builders<TEntity>.Filter;

            var filter = builder.Eq("Id", entity.Id);
            await collection.ReplaceOneAsync(filter, entity);

            return entity;
        }

        public IMongoCollection<TEntity> GetCollection()
        {
            string collectionName = resolver.Resolve(typeof(TEntity));
            var collection = db.GetCollection<TEntity>(collectionName);

            return collection;
        }
    }
}
