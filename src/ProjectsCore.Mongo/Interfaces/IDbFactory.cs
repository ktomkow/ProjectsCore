using MongoDB.Driver;

namespace ProjectsCore.Mongo.Interfaces
{
    public interface IDbFactory
    {
        IMongoDatabase Create();
    }
}
