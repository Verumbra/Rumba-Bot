namespace Rumba_Bot.Services.MongoDBSevice;

using MongoDB.Driver;

public class MongoService
{
    private string _connectionString;
    private MongoClient _client;
    private static readonly SemaphoreSlim _gate = new SemaphoreSlim(1, 1);
    
    public MongoService(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    
    
}