using Microsoft.Extensions.DependencyInjection;
using ProjectsCore.Mongo.Interfaces;
using ProjectsCore.Persistence;
using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace ProjectsCore.Mongo.IntegrationTests.RepositoryTests
{
    public class SimpleTests : TestsFixture
    {
        private readonly string collectionName;

        private readonly IRepository<int, SomePersonRepresentaion> repository;

        public SimpleTests(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            ICollectionNameResolver nameResolver = this.serviceProvider.GetService<ICollectionNameResolver>();

            this.repository = this.serviceProvider.GetService<IRepository<int, SomePersonRepresentaion>>();

            this.collectionName = nameResolver.Resolve(typeof(SomePersonRepresentaion));
        }

        [Fact]
        public async Task CreateOne()
        {
            SomePersonRepresentaion person = SomePersonRepresentaion.CreateOne();

            await this.repository.Insert(person);

            SomePersonRepresentaion personFromDb = await this.repository.Get(1);

            personFromDb.Should().NotBeNull();
            personFromDb.Should().BeEquivalentTo(person);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        public async Task CreateMany(int repeates)
        {
            for (int i = 0; i < repeates; i++)
            {
                await this.repository.Insert(SomePersonRepresentaion.CreateOne());
            }

            var peopleFromDb = (await this.repository.GetAll()).ToList();

            peopleFromDb.Should().NotBeEmpty();
        }

        protected override async Task Cleanup()
        {
            await this.collectionPurger.Purge(this.collectionName);
        }
    }
}
