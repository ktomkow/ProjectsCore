using FluentAssertions;
using ProjectsCore.Mongo.Settings;
using System.Reflection;
using Xunit;

namespace ProjectsCore.Mongo.UnitTests
{
    public class DbSettingsTests
    {
        private const string ConnectionString = "does not matter now";

        [Fact]
        public void DbName_WhenDbNameNotSpecified_UseProjectName()
        {
            MongoDbSettings dbSettings = new MongoDbSettings(ConnectionString);

            dbSettings.DbName.Should().Be(Assembly.GetExecutingAssembly().GetName().Name);
        }
    }
}
