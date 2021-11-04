using FluentAssertions;
using ProjectsCore.Models;
using Xunit;

namespace ProjectsCore.UnitTests
{
    public class ValueObjectTests
    {
        [Fact]
        public void GetHashCode_IfObjectsHaveDifferentName_ShouldBeDifferent()
        {
            SampleValueObject object1 = new SampleValueObject() { Name = "Joe", Number = 15 };
            SampleValueObject object2 = new SampleValueObject() { Name = "Doe", Number = 15 };

            object1.GetHashCode().Should().NotBe(object2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_IfCalledTwice_ShouldBeTheSame()
        {
            SampleValueObject object1 = new SampleValueObject() { Name = "Jack", Number = 124 };

            object1.GetHashCode().Should().Be(object1.GetHashCode());
        }

        private class SampleValueObject : ValueObject<SampleValueObject>
        {
            public string Name { get; set; }

            public int Number { get; set; }

            public override bool Equals(SampleValueObject other)
            {
                return this.Name == other.Name && this.Number == other.Number;
            }
        }
    }
}
