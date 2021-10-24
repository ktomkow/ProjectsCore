using ProjectsCore.Models;

namespace ProjectsCore.Mongo.IntegrationTests.RepositoryTests.Models
{
    public class IntKeyedWithEnum : Entity<int>
    {
        public Coolnes Coolnes { get; set; }
    }

    public enum Coolnes
    {
        Unknown,
        NotCool,
        Cool,
        Awesome
    }
}
