using FluentAssertions;
using ProjectsCore.Models;
using System;
using Xunit;

namespace ProjectsCore.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void EntityTests()
        {
            Guid id = Guid.Parse("34a1a454-2a13-45aa-9bc3-32172fa90f96");
            SimpleClass obj = new SimpleClass(id);

            Action act = () =>
            {
                Guid id = obj.Id;
            };

            act.Should().NotThrow();
        }

        private class SimpleClass : Entity<Guid>
        {
            public string Name { get; set; }

            public SimpleClass(Guid id)
            {
                this.id = id;
            }
        }
    }
}
