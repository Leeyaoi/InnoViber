using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace UserService.DAL.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration["DB_CONNECTION"];
        var mongoUrl = new MongoUrl(connectionString);
        var mongoClient = new MongoClient(mongoUrl);
        _database = mongoClient.GetDatabase("users");
    }

    public IMongoDatabase Database => _database;
}
