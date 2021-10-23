using Microsoft.Extensions.DependencyInjection;
using ProjectsCore.Mongo.Interfaces;
using ProjectsCore.Mongo.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectsCore.Mongo.IntegrationTests
{
    [Collection("TestsFixture")]
    public abstract class TestsFixture : IAsyncLifetime
    {
        protected readonly IServiceProvider serviceProvider;

        protected IDbFactory dbFactory => serviceProvider.GetService<IDbFactory>();
        protected ICollectionPurger collectionPurger => serviceProvider.GetService<ICollectionPurger>();

        public TestsFixture(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public virtual async Task InitializeAsync()
        {
            await Cleanup();
            await this.CleanIdentifiers();
        }

        public virtual async Task DisposeAsync()
        {
            await Cleanup();
            await this.CleanIdentifiers();
        }

        private async Task CleanIdentifiers()
        {
            await this.collectionPurger.Purge("_identifiers");
        }

        protected abstract Task Cleanup();
    }
}
