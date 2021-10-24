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
using MongoDB.Bson.Serialization.Conventions;

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

            var conventions = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            };

            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            ConventionRegistry.Register("EnumStringConvention", conventions, t => true);
        }
    }
}
