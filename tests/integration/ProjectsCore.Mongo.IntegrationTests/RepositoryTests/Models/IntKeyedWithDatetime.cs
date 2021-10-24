using ProjectsCore.Models;
using System;

namespace ProjectsCore.Mongo.IntegrationTests.RepositoryTests.Models
{
    public class IntKeyedWithDatetime : Entity<int>
    {
        public DateTime SomeDate { get; set; }
    }
}
