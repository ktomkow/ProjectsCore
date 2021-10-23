using Microsoft.Extensions.DependencyInjection;
using ProjectsCore.Mongo.ServiceProvider;
using ProjectsCore.Mongo.Settings;
using ProjectsCore.Mongo.Testing;
using System.IO;
using System.Text.Json;

namespace ProjectsCore.Mongo.IntegrationTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var settings = LoadSettings();

            var mongoSettings = new MongoDbSettings(settings.ConnectionString, "MongoPackTestDb");
            services.AddSingleton(mongoSettings);
            services.AddMongoTestingTools();
            services.AddMongo();
        }

        private static TestsSettings LoadSettings()
        {
            var settingsFile = "settings.json";
            var settingsJson = File.ReadAllText(settingsFile);

            return JsonSerializer.Deserialize<TestsSettings>(settingsJson);
        }
    }
}
