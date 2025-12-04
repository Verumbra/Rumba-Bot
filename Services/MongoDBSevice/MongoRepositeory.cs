
using MongoDB.Bson;
using MongoDB.Driver;
using Rumba_Bot.Services.ConversionUtility;

namespace Rumba_Bot.Services.MongoDBSevice;

public class UserProfileRepositeory
{
    private readonly IMongoCollection<UserProfile> _collection;
    private IDProcessor _idProcessor = new IDProcessor();

    public UserProfileRepositeory(IMongoDatabase database)
    {
        _collection = database.GetCollection<UserProfile>("UserProfile");
    }

    public async Task CreateUserProfile(UserProfile profile)
    {
        //profile.CreatedAt = DateTime.Now;
        profile.Id = ObjectId.Empty;
        
        await _collection.InsertOneAsync(profile);
    }

    public async Task<bool> UpdateUserProfile(UserProfile profile)
    {
        var result = await _collection.ReplaceOneAsync(x => x.Id == profile.Id, profile);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<UserProfile> DeleteUserProfile(UserProfile profile)
    {
        var result = await _collection.DeleteOneAsync(x => x.Id == profile.Id);
        return result.IsAcknowledged ? profile : null;
    }

    public async Task<UserProfile> GetUserProfile(UserProfile profile)
    {
        return new UserProfile(); //todo need to flesh out userProfile more to finish this function
    }
    
    
}

public class GuildProfileRepositeory
{
    private readonly IMongoCollection<GuildProfile> _collection;
    private IDProcessor _idProcessor = new IDProcessor();
    
    public GuildProfileRepositeory(IMongoDatabase database)
    {
        _collection = database.GetCollection<GuildProfile>("GuildProfile");
    }

    public async Task<bool> CheckGuildProfile(ulong id)
    {
        var results = await _collection.CountDocumentsAsync(p => p.GuildId == id);
        return results > 0;
        
    }

    public async Task<bool> CheckSetting(ulong id)
    {
        return true; //todo finish the check 
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

    public async Task<bool> UpdateGuildNameProfile(string name, ulong id)
    {
        var update = Builders<GuildProfile>.Update
            .Set(p => p.Name, name);
        
        var result = await _collection.UpdateOneAsync(p => p.GuildId == id, update);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> UpdateGuildCLChennalName(string name, ulong Id) //chat log
    {
        var update = Builders<GuildProfile>.Update
            .Set(p => p.ChatLogChennalName, name);
        
        var result = await _collection.UpdateOneAsync(p => p.GuildId == Id, update);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> UpdateGuildCLChennalId(int ChennalId, ulong GuildId) //chat log
    {
        var update = Builders<GuildProfile>.Update
            .Set(p => p.ChatLogId, ChennalId);
        var result = await _collection.UpdateOneAsync(p => p.GuildId == GuildId, update);
        
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteGuildProfile(string id)
    {
        var  result = await _collection.DeleteOneAsync(p => p.Id == _idProcessor.ToObjectId(id));
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<GuildProfile> GetGuildProfile(GuildProfile profile)
    {
        var result = await _collection
            .FindAsync(p => p.Id == profile.Id);
        return result.FirstOrDefault();
    }

    public async Task<int> GetGuildCLChennalId(ulong GuildId)  //Chat log 
    {
        var result = await _collection
            .FindAsync(p => p.GuildId == GuildId);
        var guildData = result.FirstOrDefault();
        int chennalId = guildData.ChatLogId;
        return chennalId;
    }
}

public class QuestsRepositeory
{
    private readonly IMongoCollection<Quests> _collection;
    private IDProcessor _idProcessor = new IDProcessor();

    public QuestsRepositeory(IMongoDatabase database)
    {
        _collection = database.GetCollection<Quests>("Quests");
    }

    public async Task<string> CreateQuest(Quests quest)
    {
        //quest.CreatedAt = DateTime.UtcNow;
        quest.Id = ObjectId.Empty;
        
        await _collection.InsertOneAsync(quest);
        return quest.Id.ToString();
    }

    public async Task UpdateQuest(Quests quest)
    {
        
    }

    public async Task DeleteQuest(Quests quest)
    {
        
    }

    public async Task GetQuest(Quests quest)
    {
        
    }
    
    
}