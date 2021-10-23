using System;
using System.Reflection;

namespace ProjectsCore.Mongo.Settings
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; }

        public string DbName { get; }

        public MongoDbSettings(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(ConnectionString));

            DbName = Assembly.GetCallingAssembly().GetName().Name;
        }

        public MongoDbSettings(string connectionString, string dbName)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            DbName = dbName ?? throw new ArgumentNullException(nameof(dbName));
        }
    }
}
