using FluentAssertions;
using Xunit;

namespace ProjectsCore.Reflections.UnitTests
{
    public class CallerResolverTests
    {
        [Fact]
        public void ResolveName_WhenCalledInsideMethodOfSomeKindOfClass_ThenSomeKindOfClass()
        {
            SomeKindOfClass someObject = new SomeKindOfClass();

            string result = someObject.ExecResolveName();

            result.Should().Be(nameof(SomeKindOfClass));
        }

        [Fact]
        public void ResolveFullName_WhenCalledInsideMethodOfSomeKindOfClass_ThenNamespaceAndClass()
        {
            SomeKindOfClass someObject = new SomeKindOfClass();

            string result = someObject.ExecResolveFullName();

            result.Should().Be($"ProjectsCore.Reflections.UnitTests.{nameof(SomeKindOfClass)}");
        }

        [Fact]
        public void StaticResolvedName_WhenCalledInsideMethodOfSomeKindOfClass_ThenSomeKindOfClass()
        {
            SomeKindOfClass someObject = new SomeKindOfClass();

            string result = someObject.ExecResolveName();

            result.Should().Be(nameof(SomeKindOfClass));
        }

        [Fact]
        public void StaticResolvedFullName_WhenCalledInsideMethodOfSomeKindOfClass_ThenNamespaceAndClass()
        {
            SomeKindOfClass someObject = new SomeKindOfClass();

            string result = someObject.ExecResolveFullName();

            result.Should().Be($"ProjectsCore.Reflections.UnitTests.{nameof(SomeKindOfClass)}");
        }
    }

    internal class SomeKindOfClass
    {
        internal static readonly string ResolvedName = CallerResolver.ResolveName();
        internal static readonly string ResolvedFullName = CallerResolver.ResolveFullName();

        internal string ExecResolveName()
        {
            return CallerResolver.ResolveName();
        }

        internal string ExecResolveFullName()
        {
            return CallerResolver.ResolveFullName();
        }
    }
}
