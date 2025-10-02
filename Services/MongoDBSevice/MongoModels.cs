namespace Rumba_Bot.Services.MongoDBSevice;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


[BsonIgnoreExtraElements]
public class Item
{
    
}

[BsonIgnoreExtraElements]
public sealed class UserProfile
{
    [BsonId] public ObjectId Id { get; set; }
    public string UserId { get; set; }
    public int TotalXp { get; set; }
    public List<string> Roles { get; set; }
    public List<Item> Items { get; set; }
    //todo more features need to be added.
}

[BsonIgnoreExtraElements]
public sealed class GuildProfile
{
    [BsonId] public ObjectId Id { get; set; }
    public int GuildId { get; set; }
    
    //todo need to think of setting need for the guild to function right.
}