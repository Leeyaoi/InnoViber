using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace UserService.DAL.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");
        var mongoUrl = new MongoUrl(connectionString);
        var mongoClient = new MongoClient(mongoUrl);
        _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
    }

    public IMongoDatabase Database => _database;
}
