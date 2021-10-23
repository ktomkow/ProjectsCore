using Microsoft.Extensions.DependencyInjection;
using ProjectsCore.Mongo.Interfaces;
using ProjectsCore.Persistence;
using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Linq;
using ProjectsCore.Mongo.IntegrationTests.RepositoryTests.Models;

namespace ProjectsCore.Mongo.IntegrationTests.RepositoryTests.IntKeyed
{
    public class SimpleTests : TestsFixture
    {
        private readonly string collectionName;

        private readonly IRepository<int, IntKeyedPerson> repository;

        public SimpleTests(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            ICollectionNameResolver nameResolver = this.serviceProvider.GetService<ICollectionNameResolver>();

            repository = this.serviceProvider.GetService<IRepository<int, IntKeyedPerson>>();

            collectionName = nameResolver.Resolve(typeof(IntKeyedPerson));
        }

        [Fact]
        public async Task CreateOne()
        {
            IntKeyedPerson person = IntKeyedPerson.CreateOne();

            await repository.Insert(person);

            IntKeyedPerson personFromDb = await repository.Get(1);

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
                await repository.Insert(IntKeyedPerson.CreateOne());
            }

            var peopleFromDb = (await repository.GetAll()).ToList();

            peopleFromDb.Should().NotBeEmpty();
        }

        protected override async Task Cleanup()
        {
            await collectionPurger.Purge(collectionName);
        }
    }
}
