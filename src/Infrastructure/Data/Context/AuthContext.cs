using MongoDB.Driver;
using StudentCenterAuthApi.src.Domain.Model;

namespace StudentCenterAuthApi.src.Infrastructure.Data.Context;

public class AuthContext
{
    private readonly IMongoDatabase _database;

    public AuthContext(IConfiguration configuration)
    {
        var connectionString = configuration["MongoDB:ConnectionString"];
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("User");
}
