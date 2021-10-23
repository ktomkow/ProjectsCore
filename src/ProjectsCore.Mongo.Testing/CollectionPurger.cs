using MongoDB.Bson;
using MongoDB.Driver;
using ProjectsCore.Mongo.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjectsCore.Mongo.Testing
{
    public class CollectionPurger : ICollectionPurger
    {
        private readonly IMongoDatabase db;

        public CollectionPurger(IDbFactory dbFactory)
        {
            db = dbFactory.Create();
        }

        public Task Purge()
        {
            throw new NotImplementedException();
        }

        public async Task Purge(string collection)
        {
            var dbCollection = db.GetCollection<object>(collection);
            await dbCollection.DeleteManyAsync(new BsonDocument());
        }
    }
}
