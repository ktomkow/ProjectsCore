using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProjectsCore.Models;
using ProjectsCore.Mongo.IntegrationTests.RepositoryTests.Models;
using ProjectsCore.Mongo.Interfaces;
using ProjectsCore.Persistence;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectsCore.Mongo.IntegrationTests.RepositoryTests.IntKeyed
{
    public class ReplaceTests : TestsFixture
    {
        private readonly string collectionName;

        private readonly IRepository<int, IntKeyedPerson> repository;

        public ReplaceTests(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            ICollectionNameResolver nameResolver = this.serviceProvider.GetService<ICollectionNameResolver>();

            repository = this.serviceProvider.GetService<IRepository<int, IntKeyedPerson>>();

            collectionName = nameResolver.Resolve(typeof(IntKeyedPerson));
        }

        [Fact]
        public async Task UpdateOne()
        {
            var person = IntKeyedPerson.CreateOne();

            await repository.Insert(person);

            var personFromDb = await repository.Get(1);

            personFromDb.FirstName = "John";
            personFromDb.LastName = "Doe";

            await repository.Update(personFromDb);

            var updatedPerson = await repository.Get(1);

            updatedPerson.FirstName.Should().Be("John");
            updatedPerson.LastName.Should().Be("Doe");
        }

        public class GuidKeyed : Entity<Guid>
        {
            public string Name { get; set; }
        }

        protected override async Task Cleanup()
        {
            await collectionPurger.Purge(collectionName);
        }
    }
}
