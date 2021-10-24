using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjectsCore.Mongo.IntegrationTests.RepositoryTests.Models;
using ProjectsCore.Mongo.Interfaces;
using ProjectsCore.Persistence;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectsCore.Mongo.IntegrationTests.RepositoryTests.IntKeyed
{

    public class InsertTests : TestsFixture
    {
        private readonly IRepository<int, IntKeyedWithEnum> withEnumRepository;

        public InsertTests(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.withEnumRepository = this.serviceProvider.GetService<IRepository<int, IntKeyedWithEnum>>();
        }


        [Fact]
        public async Task Insert_IfEnum_ShouldBeStoredAsString()
        {
            IntKeyedWithEnum entity = new IntKeyedWithEnum()
            {
                Coolnes = Coolnes.Awesome
            };

            await this.withEnumRepository.Insert(entity);

            IDbFactory dbFactory = this.serviceProvider.GetService<IDbFactory>();
            ICollectionNameResolver resolver = this.serviceProvider.GetService<ICollectionNameResolver>();

            var db = dbFactory.Create();
            string collectionName = resolver.Resolve(entity);
            var collection = db.GetCollection<BsonDocument>(collectionName);

            BsonDocument storedEntity = collection.AsQueryable().First();
            var coolnes = storedEntity.GetValue(nameof(Coolnes));
            coolnes.ToString().Should().Be(nameof(Coolnes.Awesome));
        }

        protected override async Task Cleanup()
        {
            await this.collectionPurger.Purge(nameof(IntKeyedWithEnum));
        }
    }
}
