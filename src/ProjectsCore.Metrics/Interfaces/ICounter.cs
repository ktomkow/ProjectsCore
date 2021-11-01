using System.Threading.Tasks;

namespace ProjectsCore.Metrics.Interfaces
{
    public interface ICounter
    {
        Task Tick();
        Task Tick(uint count);
        Task Tick(double count);
    }
}
