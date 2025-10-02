using MongoDB.Driver;

namespace Rumba_Bot.Services.MongoDBSevice;

public class UserProfileRepositeory
{
    private readonly IMongoCollection<UserProfile> _collection;

    public UserProfileRepositeory(IMongoDatabase database)
    {
        _collection = database.GetCollection<UserProfile>("UserProfile");
    }
    
    
}