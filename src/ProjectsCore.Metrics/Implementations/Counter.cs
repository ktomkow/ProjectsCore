using ProjectsCore.Metrics.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjectsCore.Metrics.Implementations
{
    public class Counter : ICounter
    {
        public Task Tick()
        {
            throw new NotImplementedException();
        }

        public Task Tick(uint count)
        {
            throw new NotImplementedException();
        }

        public Task Tick(double count)
        {
            throw new NotImplementedException();
        }
    }
}
