using Microsoft.Extensions.DependencyInjection;

namespace ProjectsCore.Mongo.Testing
{
    public static class ServiceCollectionExtension
    {
        public static void AddMongoTestingTools(this IServiceCollection services)
        {
            services.AddTransient<ICollectionPurger, CollectionPurger>();
        }
    }
}
