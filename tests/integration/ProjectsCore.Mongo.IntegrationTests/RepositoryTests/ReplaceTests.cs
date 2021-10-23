using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProjectsCore.Mongo.Interfaces;
using ProjectsCore.Persistence;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectsCore.Mongo.IntegrationTests.RepositoryTests
{
    public class ReplaceTests : TestsFixture
    {
        private readonly string collectionName;

        private readonly IRepository<int, SomePersonRepresentaion> repository;

        public ReplaceTests(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            ICollectionNameResolver nameResolver = this.serviceProvider.GetService<ICollectionNameResolver>();

            this.repository = this.serviceProvider.GetService<IRepository<int, SomePersonRepresentaion>>();

            this.collectionName = nameResolver.Resolve(typeof(SomePersonRepresentaion));
        }

        [Fact]
        public async Task UpdateOne()
        {
            var person = SomePersonRepresentaion.CreateOne();

            await this.repository.Insert(person);

            var personFromDb = await this.repository.Get(1);

            personFromDb.FirstName = "John";
            personFromDb.LastName = "Doe";

            await this.repository.Update(personFromDb);

            var updatedPerson = await this.repository.Get(1);

            updatedPerson.FirstName.Should().Be("John");
            updatedPerson.LastName.Should().Be("Doe");
        }

        protected override async Task Cleanup()
        {
            await this.collectionPurger.Purge(this.collectionName);
        }
    }
}
