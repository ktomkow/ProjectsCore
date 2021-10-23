using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProjectsCore.IdGeneration;
using ProjectsCore.Models;
using ProjectsCore.Mongo.Implementations.IdGeneration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProjectsCore.Mongo.IntegrationTests.IdGenerationTests
{
    public class IntIdGenerationTests : TestsFixture, IAsyncLifetime
    {
        private readonly EntityIntIdGenerator generator;

        public IntIdGenerationTests(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            generator = (EntityIntIdGenerator)this.serviceProvider.GetService<IEntityIdGenerator<int>>();
        }

        [Fact]
        public async Task GenerateThreeIdentifiersOneByOne()
        {
            var type = typeof(IntKeyedClass);
            int firstId = await generator.Generate(type);
            int secondId = await generator.Generate(type);
            int thirdId = await generator.Generate(type);

            firstId.Should().Be(1);
            secondId.Should().Be(2);
            thirdId.Should().Be(3);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public async Task CheckRaceConditions(int repeats)
        {
            var type = typeof(IntKeyedClass);
            var list = new List<Task>();

            for (int i = 0; i < repeats; i++)
            {
                list.Add(generator.Generate(type));
            }

            await Task.WhenAll(list);

            int lastOne = await generator.Generate(type);

            lastOne.Should().Be(repeats + 1);
        }

        protected override async Task Cleanup()
        {
            await collectionPurger.Purge("_identifiers");
        }

        public class IntKeyedClass : Entity<int>
        {
            public string Name { get; set; }
        }
    }
}
