using System.Threading.Tasks;

namespace ProjectsCore.Metrics.Interfaces
{
    public interface ICounter
    {
        Task Tick();
        Task Tick(string series);
        Task Tick(uint count);
        Task Tick(uint count, string series);
    }
}
