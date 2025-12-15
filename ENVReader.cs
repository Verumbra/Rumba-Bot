namespace Rumba_Bot;

public class EnvReader
{
    public string Token {get; set;}
    public string Prefix {get; set;}
    
    public string RedisHost {get; set;}
    public string RedisPort {get; set;}
    public string RedisUsername {get; set;}
    public string RedisPassword {get; set;}
    public string MongoHost {get; set;}
    public string MongoPort {get; set;}
    public string MongoUsername {get; set;}
    public string MongoPassword {get; set;}
    public string MongoDatabase {get; set;}

    public EnvReader()
    {
        try
        {
            Token = Environment.GetEnvironmentVariable("TOKEN");
            Prefix = Environment.GetEnvironmentVariable("PREFIX");
            RedisHost = Environment.GetEnvironmentVariable("REDIS_HOST");
            RedisPort = Environment.GetEnvironmentVariable("REDIS_PORT");
            RedisUsername = Environment.GetEnvironmentVariable("REDIS_USERNAME");
            RedisPassword = Environment.GetEnvironmentVariable("REDIS_PASSWORD");
            MongoHost = Environment.GetEnvironmentVariable("MONGO_HOST");
            MongoPort = Environment.GetEnvironmentVariable("MONGO_PORT");
            MongoUsername = Environment.GetEnvironmentVariable("MONGO_USERNAME");
            MongoPassword = Environment.GetEnvironmentVariable("MONGO_PASSWORD");
            MongoDatabase = Environment.GetEnvironmentVariable("MONGO_DB");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}

