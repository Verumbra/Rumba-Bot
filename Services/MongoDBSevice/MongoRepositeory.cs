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

public class GuildProfileRepositeory
{
    private readonly IMongoCollection<GuildProfile> _collection;

    public GuildProfileRepositeory(IMongoDatabase database)
    {
        _collection = database.GetCollection<GuildProfile>("GuildProfile");
    }
    
}

public class QuestsRepositeory
{
    private readonly IMongoCollection<Quests> _collection;

    public QuestsRepositeory(IMongoDatabase database)
    {
        _collection = database.GetCollection<Quests>("Quests");
    }
    
    
}