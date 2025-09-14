namespace Rumba_Bot.Services.RedisService;

using StackExchange.Redis;
using DotNetEnv;



public class RedisServices
{
    
    private string _connectionstring; //must be in the format of "" or "host:port,user=app,password=PASSWORD"
    private ConnectionMultiplexer? _mux;
    private static readonly SemaphoreSlim _gate = new SemaphoreSlim(1, 1);

    public RedisServices(string connstring, bool isProd)
    {
        _connectionstring = connstring;
    }

    public async Task StartConnection()
    {
        if (_mux?.IsConnected == true) return;
        
        await _gate.WaitAsync();
        
        try
        {
            if (_mux?.IsConnected == true) return;
            CancellationTokenSource cts = new CancellationTokenSource();
            var ct  = cts.Token;
            var options = ConfigurationOptions.Parse(_connectionstring);
            options.AbortOnConnectFail = false;
            options.ConnectRetry = 3;
            options.ConnectTimeout = 5000;
            
            Exception? last = null;
            for (int attempt = 1; attempt <= 3; attempt++)
            {
                try
                {
                    _mux = await ConnectionMultiplexer.ConnectAsync(options)
                        .WaitAsync(TimeSpan.FromSeconds(15),ct);
                    
                    _mux.ConnectionFailed += (_, args) =>
                        Console.Error.WriteLine($"RedisServices connection failed: {args.EndPoint}");
                    _mux.ConnectionRestored += (_, args) =>
                        Console.Error.WriteLine($"RedisServices connection restored: {args.EndPoint}");
                    return;
                }
                catch (Exception e) when (attempt < 3 && !cts.IsCancellationRequested)
                {
                    last = e;
                    await Task.Delay(TimeSpan.FromSeconds(1 << attempt), ct);
                }
            }
            throw last ?? throw new Exception("No connection could be established"); 
        }
        finally
        {
           _gate.Release();
        }
    }
    public IDatabase GetDatabase(int db = 0) =>
        _mux?.GetDatabase(db) ?? throw new InvalidOperationException("Call StartConnection first.");

    public async Task CloseConnection()
    {
        var m = _mux;
        _mux =  null;
        if (m != null)  await m.CloseAsync();
        m?.Dispose();
    }


    public async Task SaveMessage(string userIdCode, string message)
    {
        string userKey = $"{userIdCode}";
        string userMessage = message; //todo need checks 
        
        var redis = GetDatabase();
        try
        {
            await redis.StringSetAsync(userKey, userMessage);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<String> RetrieveMessage(string userKey)
    {
        var redis = GetDatabase();
        try
        {
            RedisValue result = await redis.StringGetAsync(userKey);
            Console.WriteLine("here is result for the retrieval ",result);
            if (result.IsNullOrEmpty)
                return "No saved message found, sorry.";
            return result.ToString();
        }
        catch (Exception e)
        {
            return "No message found!";
        }
        
    }
    
}