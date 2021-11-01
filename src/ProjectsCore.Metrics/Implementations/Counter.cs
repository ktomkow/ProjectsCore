using ProjectsCore.Metrics.Interfaces;
using ProjectsCore.Reflections;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ProjectsCore.Metrics.Implementations
{
    public class Counter : ICounter
    {
        private static ConcurrentDictionary<string, Prometheus.Counter> counters = new ConcurrentDictionary<string, Prometheus.Counter>();

        public Task Tick()
        {
            var counter = ResolveCounter(CallerResolver.ResolveFullName());

            counter.Inc();

            return Task.CompletedTask;
        }

        public Task Tick(string series)
        {
            var counter = ResolveCounter(CallerResolver.ResolveFullName(), series);

            counter.Inc();

            return Task.CompletedTask;
        }

        public Task Tick(uint count)
        {
            var counter = ResolveCounter(CallerResolver.ResolveFullName());

            counter.Inc(count);

            return Task.CompletedTask;
        }

        public Task Tick(uint count, string series)
        {
            var counter = ResolveCounter(CallerResolver.ResolveFullName(), series);

            counter.Inc(count);

            return Task.CompletedTask;
        }

        private Prometheus.Counter ResolveCounter(string caller)
        {
            return counters.GetOrAdd(caller, Prometheus.Metrics.CreateCounter(caller, "AUTOMATICALLY_GENERATED"));
        }

        private Prometheus.Counter ResolveCounter(string caller, string series)
        {
            return counters.GetOrAdd(caller, Prometheus.Metrics.CreateCounter($"{caller}:{series}", "AUTOMATICALLY_GENERATED"));
        }
    }
}
