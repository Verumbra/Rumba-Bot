namespace Rumba_Bot.Services.MongoDBSevice;

using MongoDB.Driver;

public class MongoService
{
    private string _connectionString;
    private MongoClientSettings _settings;
    private MongoClient _client;
    private static readonly SemaphoreSlim _gate = new SemaphoreSlim(1, 1);
    
    public MongoService(string connectionString)
    {
        _connectionString = connectionString;
        _settings = MongoClientSettings.FromConnectionString(_connectionString);
        
        _settings.MaxConnectionPoolSize = 100;
        _settings.MinConnectionPoolSize = 3;
        _settings.MaxConnecting = 4;
        _settings.MaxConnectionIdleTime = TimeSpan.FromMinutes(8);
        _settings.MaxConnectionLifeTime = TimeSpan.FromMinutes(40);
        _settings.WaitQueueTimeout = TimeSpan.FromMinutes(1);
        _client = new MongoClient(_settings);
    }

    public MongoService(string connectionString, int maxPoolSize, int minPoolSize, int maxConnecting, int maxConnectionIdleTime)
    {
        _connectionString = connectionString;
        _settings = MongoClientSettings.FromConnectionString(_connectionString);
        
        _settings.MaxConnectionPoolSize = maxPoolSize;
        _settings.MinConnectionPoolSize = minPoolSize;
        _settings.MaxConnecting = maxConnecting;
        _settings.MaxConnectionIdleTime = TimeSpan.FromMinutes(maxConnectionIdleTime);
        _settings.MaxConnectionLifeTime = TimeSpan.FromMinutes(40);
        _settings.WaitQueueTimeout = TimeSpan.FromMinutes(1);
        _client = new MongoClient(_settings);
        
    }
    
    
    
    
}