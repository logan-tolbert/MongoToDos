using MongoDB.Driver;

namespace MongoDbClient.Lib;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    // Option 1: Use a full connection string
    public MongoDbContext(string connectionString)
    {
        var mongoUrl = new MongoUrl(connectionString);
        var client = new MongoClient(mongoUrl);
        _database = client.GetDatabase(mongoUrl.DatabaseName 
            ?? throw new ArgumentException("Database name must be included in the connection string."));
    }

    // Option 2: Manually pass host, port, and db name
    public MongoDbContext(string host, int port, string database)
    {
        var settings = new MongoClientSettings
        {
            Server = new MongoServerAddress(host, port)
        };
        var client = new MongoClient(settings);
        _database = client.GetDatabase(database);
    }

    // Option 3: Use environment variables (fallback/default)
    public MongoDbContext()
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");

        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            var mongoUrl = new MongoUrl(connectionString);
            var client = new MongoClient(mongoUrl);
            _database = client.GetDatabase(mongoUrl.DatabaseName 
                ?? throw new ArgumentException("Database name must be included in the connection string."));
        }
        else
        {
            var host = Environment.GetEnvironmentVariable("MONGO_HOST") ?? "localhost";
            var portStr = Environment.GetEnvironmentVariable("MONGO_PORT") ?? "27017";
            var dbName = Environment.GetEnvironmentVariable("MONGO_DATABASE") ?? "defaultDb";

            if (!int.TryParse(portStr, out var port))
                throw new InvalidOperationException("Invalid MONGO_PORT environment variable.");

            var settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(host, port)
            };
            var client = new MongoClient(settings);
            _database = client.GetDatabase(dbName);
        }
    }

    public IMongoCollection<TDocument> LoadCollection<TDocument>(string collectionName)
    {
        return _database.GetCollection<TDocument>(collectionName);
    }
}
