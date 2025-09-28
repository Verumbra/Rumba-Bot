namespace Rumba_Bot.Services.MongoDBSevice;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public sealed class UserProfile
{
    [BsonId] public ObjectId Id { get; set; }
    public string UserId { get; set; }
    public int TotalXp { get; set; }
    public List<string> Roles { get; set; }
    //todo more features need to be added.
}