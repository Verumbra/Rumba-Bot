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
    
    [BsonRepresentation(BsonType.Int64, AllowOverflow = true)]
    public ulong GuildId { get; set; }
    public string Name { get; set; }
    public string ChatLogChennalName { get; set; }
    
    [BsonRepresentation(BsonType.Int64, AllowOverflow = true)]
    public int ChatLogId { get; set; }
    
    //todo need to think of setting need for the guild to function right.
}

[BsonIgnoreExtraElements]
public sealed class Quests
{
    [BsonId] public ObjectId Id { get; set; }
    public int QuestId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int MinRewardGold { get; set; }
    public int MaxRewardGold { get; set; }
    public List<Encounters> Steps { get; set; }
}

[BsonIgnoreExtraElements]
public sealed class Encounters
{
    [BsonId] public ObjectId Id { get; set; }
}