using MongoPack.IdGeneration;
using System;
using ProjectsCore.Persistence;
using ProjectsCore.Mongo.Implementations;
using ProjectsCore.Mongo.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ProjectsCore.IdGeneration;
using ProjectsCore.Mongo.Implementations.IdGeneration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;

namespace ProjectsCore.Mongo.ServiceProvider
{
    public static class ServiceCollectionExtension
    {
        public static void AddMongo(this IServiceCollection services)
        {
            services.AddTransient<IDbFactory, DbFactory>();
            services.AddTransient<ICollectionNameResolver, DefaultCollectionNameResolver>();

            services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddTransient<IEntityIdGenerator<int>, EntityIntIdGenerator>();
            services.AddTransient<IEntityIdGenerator<Guid>, EntityGuidIdGenerator>();

            BsonSerializer.RegisterSerializer(typeof(DateTime), new DateTimeSerializer(DateTimeKind.Utc, BsonType.DateTime));
        }
    }
}
