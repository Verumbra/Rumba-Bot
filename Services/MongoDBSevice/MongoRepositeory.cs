using MongoDB.Bson;
using MongoDB.Driver;

namespace Rumba_Bot.Services.MongoDBSevice;

public class UserProfileRepositeory
{
    private readonly IMongoCollection<UserProfile> _collection;

    public UserProfileRepositeory(IMongoDatabase database)
    {
        _collection = database.GetCollection<UserProfile>("UserProfile");
    }

    public async Task<UserProfile> CreateUserProfile(UserProfile profile)
    {
        profile.CreatedAt = DateTime.Now;
        profile.Id = ObjectId.Empty;
        
        await _collection.InsertOneAsync(profile);
    }

    public async Task<UserProfile> UpdateUserProfile(UserProfile profile)
    {
        
    }

    public async Task<UserProfile> DeleteUserProfile(UserProfile profile)
    {
        
    }

    public async Task<UserProfile> GetUserProfile(UserProfile profile)
    {
        
    }
    
    
}

public class GuildProfileRepositeory
{
    private readonly IMongoCollection<GuildProfile> _collection;

    public GuildProfileRepositeory(IMongoDatabase database)
    {
        _collection = database.GetCollection<GuildProfile>("GuildProfile");
    }

    public async Task<GuildProfile> CreateGuildProfile(GuildProfile profile)
    {
        profile.CreatedAt = DateTime.UtcNow;
        profile.Id = null;
        
        await _collection.InsertOneAsync(profile);
        
        return profile;
    }

    public async Task<GuildProfile> UpdateGuildProfile(GuildProfile profile)
    {
        
    }

    public async Task<GuildProfile> DeleteGuildProfile(GuildProfile profile)
    {
        
    }

    public async Task<GuildProfile> GetGuildProfile(GuildProfile profile)
    {
        
    }
}

public class QuestsRepositeory
{
    private readonly IMongoCollection<Quests> _collection;

    public QuestsRepositeory(IMongoDatabase database)
    {
        _collection = database.GetCollection<Quests>("Quests");
    }

    public async Task<Quests> CreateQuest(Quests quest)
    {
        quest.CreatedAt = DateTime.UtcNow;
        quest.Id = ObjectId.Empty;
        
        await _collection.InsertOneAsync(quest);
    }

    public async Task<Quests> UpdateQuest(Quests quest)
    {
        
    }

    public async Task<Quests> DeleteQuest(Quests quest)
    {
        
    }

    public async Task<Quests> GetQuest(Quests quest)
    {
        
    }
    
    
}