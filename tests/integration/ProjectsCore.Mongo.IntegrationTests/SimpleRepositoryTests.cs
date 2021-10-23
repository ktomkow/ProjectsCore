using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using ProjectsCore.Models;
using System;
using System.Threading.Tasks;
using Xunit;
using ProjectsCore.Persistence;
using System.Linq;
using ProjectsCore.Mongo.Interfaces;

namespace ProjectsCore.Mongo.IntegrationTests
{
    public class SimpleRepositoryTests : TestsFixture
    {
        private readonly IRepository<int, SimpleEntity> repositoryIntKey;
        private readonly IRepository<Guid, SimpleEntityGuidId> repositoryGuidKey;

        private readonly string collectionNameInt;
        private readonly string collectionNameGuid;

        public SimpleRepositoryTests(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            ICollectionNameResolver nameResolver = this.serviceProvider.GetService<ICollectionNameResolver>();

            this.repositoryIntKey = this.serviceProvider.GetService<IRepository<int, SimpleEntity>>();
            this.repositoryGuidKey = this.serviceProvider.GetService<IRepository<Guid, SimpleEntityGuidId>>();

            this.collectionNameInt = nameResolver.Resolve(typeof(SimpleEntity));
            this.collectionNameGuid = nameResolver.Resolve(typeof(SimpleEntityGuidId));
        }

        [Fact]
        public async Task Insert_IntId_ShouldBeGenerated()
        {
            SimpleEntity entity = new SimpleEntity()
            {
                Name = "Naaaame"
            };

            await repositoryIntKey.Insert(entity);

            entity.Id.Should().NotBe(default);
        }

        [Fact]
        public async Task InsertAndRead_IntId_ShouldBeGenerated()
        {
            SimpleEntity entity = new SimpleEntity()
            {
                Name = "Naaaame"
            };

            await repositoryIntKey.Insert(entity);
            var entities = await repositoryIntKey.GetAll();
            var readEntity = entities.First();

            readEntity.Id.Should().NotBe(default);
        }

        [Fact]
        public async Task Insert_GuidId_ShouldBeGenerated()
        {
            SimpleEntityGuidId entity = new SimpleEntityGuidId()
            {
                Name = "Naaaame"
            };

            await repositoryGuidKey.Insert(entity);

            entity.Id.Should().NotBe(default(Guid));
        }

        protected override async Task Cleanup()
        {
            await this.collectionPurger.Purge(collectionNameInt);
            await this.collectionPurger.Purge(collectionNameGuid);
        }

        private class SimpleEntity : Entity<int>
        {
            public string Name { get; set; }
        }

        private class SimpleEntityGuidId : Entity<Guid>
        {
            public string Name { get; set; }
        }
    }
}
