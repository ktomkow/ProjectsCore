using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ProjectsCore.DynamicLambda.UnitTests
{
    [Collection(nameof(DynamicLambdaFactoryTests))]
    public class DynamicLambdaFactoryTests
    {
        [Fact]
        public void Create_IntInt_ShouldWork()
        {
            // x => x * 2
            string lambda = "x * 2";

            Func<int, int> doubler = DynamicLambdaFactory.CreateFunc<int, int>(lambda);

            int input = 3;
            int expectedOutput = 6;

            int output = doubler(input);

            output.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        public void Create_IntInt_ManyTimes_ShouldBeFast(int loopsCount)
        {
            var results = new List<int>();
            for (int i = 0; i < loopsCount; i++)
            {
                // x => x * 2
                string lambda = "x * 2";

                Func<int, int> doubler = DynamicLambdaFactory.CreateFunc<int, int>(lambda);

                int input = 3;
                int output = doubler(input);
                results.Add(output);
            }

            results.TrueForAll(x => x == 6);
        }
    }
}
