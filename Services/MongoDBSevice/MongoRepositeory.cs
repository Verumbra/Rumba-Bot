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
        //profile.CreatedAt = DateTime.Now;
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
        //profile = DateTime.UtcNow;
        profile.Id = ObjectId.Empty;
        
        await _collection.InsertOneAsync(profile);
        
        return profile;
    }

    public async Task<IEnumerable<GuildProfile>> BatchCreateGuildProfile(IEnumerable<GuildProfile> profiles)
    {
        profiles = profiles.ToArray();

        foreach (var profile in profiles)
        {
            profile.Id = ObjectId.Empty;
        }
        await _collection.InsertManyAsync(profiles);
        return profiles;
    }

    public async Task<bool> UpdateFullGuildProfile(GuildProfile profile) //full override
    {
        var result = await _collection.ReplaceOneAsync(p => p.Id == profile.Id, profile);
        
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> UpdateGuildNameProfile(string name)
    {
        
    }

    public async Task<bool> UpdateGuildCLChennalName(string name, int Id)
    {
        var update = Builders<GuildProfile>.Update
            .Set(p => p.ChatLogChennalName, name);
        
        var result = await _collection.UpdateOneAsync(p => p.GuildId == Id, update);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> UpdateGuildCLChennalId(int id)
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
        //quest.CreatedAt = DateTime.UtcNow;
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