using Microsoft.Extensions.DependencyInjection;
using ProjectsCore.Models;
using ProjectsCore.Mongo.Interfaces;
using ProjectsCore.Persistence;
using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Text;
using Bogus;
using System.Linq;

namespace ProjectsCore.Mongo.IntegrationTests.RepositoryTests
{
    public class RepositoryTests : TestsFixture
    {
        private readonly string collectionName;

        private readonly IRepository<int, SomePersonRepresentaion> repository;

        public RepositoryTests(IServiceProvider serviceProvider) : base(serviceProvider)
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

        public class SomePersonRepresentaion : Entity<int>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Guid MemberIdentifier { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public int LoyaltyPoints { get; set; }
            public decimal Credits { get; set; }

            public static SomePersonRepresentaion CreateOne()
            {
                var faker = new Faker<SomePersonRepresentaion>()
                    .StrictMode(true)
                    .RuleFor(x => x.Id, y => 0)
                    .RuleFor(x => x.FirstName, y => y.Person.FirstName)
                    .RuleFor(x => x.LastName, y => y.Person.LastName)
                    .RuleFor(x => x.MemberIdentifier, y => Guid.NewGuid())
                    .RuleFor(x => x.Phone, y => y.Phone.PhoneNumber())
                    .RuleFor(x => x.Email, y => y.Person.Email)
                    .RuleFor(x => x.LoyaltyPoints, y => y.Random.Int(0, int.MaxValue))
                    .RuleFor(x => x.Credits, y => y.Finance.Amount(0, decimal.MaxValue));

                return faker.Generate();
            }

            public override string ToString()
            {
                var builder = new StringBuilder();
                builder.AppendLine($"{nameof(SomePersonRepresentaion.FirstName)} : {FirstName}");
                builder.AppendLine($"{nameof(SomePersonRepresentaion.LastName)} : {LastName}");
                builder.AppendLine($"{nameof(SomePersonRepresentaion.MemberIdentifier)} : {MemberIdentifier}");
                builder.AppendLine($"{nameof(SomePersonRepresentaion.Email)} : {Email}");
                builder.AppendLine($"{nameof(SomePersonRepresentaion.LoyaltyPoints)} : {LoyaltyPoints}");
                builder.AppendLine($"{nameof(SomePersonRepresentaion.Credits)} : {Credits}");
                builder.AppendLine($"{nameof(SomePersonRepresentaion.Phone)} : {Phone}");

                return builder.ToString();
            }
        }

        protected override async Task Cleanup()
        {
            await this.collectionPurger.Purge(this.collectionName);
        }
    }
}
