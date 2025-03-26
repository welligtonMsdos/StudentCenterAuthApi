using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StudentCenterAuthApi.src.Domain.Model;

public class User
{
    [BsonId] // Mapeia o campo "_id" do MongoDB
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PassWord { get; set; }
    public bool FirstAccess { get; set; }
    public DateTime LastAccess { get; set; }
    public bool Active { get; set; }
}
