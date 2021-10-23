using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProjectsCore.Mongo.IntegrationTests.RepositoryTests.Models;
using ProjectsCore.Mongo.Interfaces;
using ProjectsCore.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProjectsCore.Mongo.IntegrationTests.RepositoryTests.GuidKeyed
{
    public class GetTests : TestsFixture
    {
        private readonly string collectionName;

        private readonly IRepository<Guid, GuidKeyedPerson> repository;

        public GetTests(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            ICollectionNameResolver nameResolver = this.serviceProvider.GetService<ICollectionNameResolver>();

            this.repository = this.serviceProvider.GetService<IRepository<Guid, GuidKeyedPerson>>();

            this.collectionName = nameResolver.Resolve(typeof(IntKeyedPerson));
        }

        [Fact]
        public async Task CreateOne()
        {
            GuidKeyedPerson person = GuidKeyedPerson.CreateOne();

            Guid key = (await this.repository.Insert(person)).Id;

            GuidKeyedPerson personFromDb = await this.repository.Get(key);

            personFromDb.Should().NotBeNull();
            personFromDb.Should().BeEquivalentTo(person);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        public async Task CreateMany(int repeats)
        {
            for (int i = 0; i < repeats; i++)
            {
                await this.repository.Insert(GuidKeyedPerson.CreateOne());
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
