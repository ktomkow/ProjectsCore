using Microsoft.Extensions.DependencyInjection;
using ProjectsCore.Metrics.Implementations;
using ProjectsCore.Metrics.Interfaces;

namespace ProjectsCore.Metrics.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMetrics(this IServiceCollection services)
        {
            services.AddTransient<ICounter, Counter>();
        }
    }
}
