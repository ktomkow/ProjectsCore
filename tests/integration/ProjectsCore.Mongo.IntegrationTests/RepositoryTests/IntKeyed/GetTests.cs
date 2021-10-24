using Microsoft.Extensions.DependencyInjection;
using ProjectsCore.Persistence;
using ProjectsCore.Extensions;
using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Linq;
using ProjectsCore.Mongo.IntegrationTests.RepositoryTests.Models;

namespace ProjectsCore.Mongo.IntegrationTests.RepositoryTests.IntKeyed
{
    public class GetTests : TestsFixture
    {
        private readonly IRepository<int, IntKeyedPerson> repository;
        private readonly IRepository<int, IntKeyedWithDatetime> withDatetimeRepository;

        public GetTests(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            repository = this.serviceProvider.GetService<IRepository<int, IntKeyedPerson>>();
            withDatetimeRepository = this.serviceProvider.GetService<IRepository<int, IntKeyedWithDatetime>>();
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
        public async Task CreateMany(int repeats)
        {
            for (int i = 0; i < repeats; i++)
            {
                await repository.Insert(IntKeyedPerson.CreateOne());
            }

            var peopleFromDb = (await repository.GetAll()).ToList();

            peopleFromDb.Should().NotBeEmpty();
        }

        [Fact]
        public async Task InsertAndGet_IfDatetime_ShouldBeCloseToEachOther()
        {
            IntKeyedWithDatetime entity = new IntKeyedWithDatetime()
            {
                SomeDate = new DateTime(2020, 1, 23, 10, 10, 59, DateTimeKind.Utc)
            };

            await this.withDatetimeRepository.Insert(entity);

            var entityFromDb = await this.withDatetimeRepository.Get(1);

            bool areEqual = entity.SomeDate.IsEqualTo(entityFromDb.SomeDate);
            areEqual.Should().BeTrue();
        }

        protected override async Task Cleanup()
        {
            await collectionPurger.Purge(nameof(IntKeyedPerson));
            await collectionPurger.Purge(nameof(IntKeyedWithDatetime));
        }
    }
}
