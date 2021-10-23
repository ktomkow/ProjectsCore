using FluentAssertions;
using ProjectsCore.Mongo.Implementations;
using ProjectsCore.Mongo.Interfaces;
using Xunit;

namespace MongoPack.UnitTests
{
    public class DefaultCollectionNameResolverTests
    {
        private readonly ICollectionNameResolver resolver;

        public DefaultCollectionNameResolverTests()
        {
            this.resolver = new DefaultCollectionNameResolver();
        }

        [Fact]
        public void Resolve_SomeClass_ShouldMatch_SomeClass()
        {
            SomeClass someInstance = new SomeClass();

            string result = this.resolver.Resolve(someInstance);

            result.Should().Be("SomeClass");
        }

        [Fact]
        public void Resolve_OtherClass_ShouldMatch_OtherClass()
        {
            OtherClass someInstance = new OtherClass();

            string result = this.resolver.Resolve(someInstance);

            result.Should().Be("OtherClass");
        }

        [Fact]
        public void Resolve_ChildClassInstanceButBaseClassInstance_ShouldMatch_ChildClass()
        {
            SomeClass someInstance = new ChildClass();

            string result = this.resolver.Resolve(someInstance);

            result.Should().Be("ChildClass");
        }

        [Fact]
        public void Resolve_ChildClass_ShouldMatch_ChildClass()
        {
            ChildClass someInstance = new ChildClass();

            string result = this.resolver.Resolve(someInstance);

            result.Should().Be("ChildClass");
        }


        [Fact]
        public void ResolveByType_SomeClass_ShouldMatch_SomeClass()
        {
            string result = this.resolver.Resolve(typeof(SomeClass));

            result.Should().Be("SomeClass");
        }

        private class SomeClass
        {

        }

        private class OtherClass
        {

        }

        private class ChildClass : SomeClass
        {

        }
    }
}
