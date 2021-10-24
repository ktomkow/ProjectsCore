using System;
using Xunit;
using ProjectsCore.Extensions;
using FluentAssertions;

namespace ProjectsCore.UnitTests.ExtensionTests
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void IsEqualTo_WhenCompareToItself_ThenTrue()
        {
            DateTime now = DateTime.UtcNow;

            bool result = now.IsEqualTo(now);

            result.Should().BeTrue();
        }
    }
}
