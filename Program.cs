// See https://aka.ms/new-console-template for more information


using Rumba_Bot.Services.MongoDBSevice;
using Rumba_Bot.Services.RedisService;
using Rumba_Bot.Services.StringUtility;
using MongoDB.Driver;

namespace Rumba_Bot;



class DiscordBot
{
    private static UserInputProcessor InputParser = new UserInputProcessor();
    private static DiscordClient Client { get; set; }
    private static EnvReader EnvValues = new EnvReader();
    
    
    
    
    static async Task Main(string[] args)
    {
        try
        {
            Env.Load();

            if (Environment.GetEnvironmentVariable("TOKEN") == null)
            {
                Env.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env"));
            }
            
            if (Environment.GetEnvironmentVariable("TOKEN") == null)
            {
                var projectDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.FullName;
                if (projectDir != null)
                {
                    Env.Load(Path.Combine(projectDir, ".env"));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading .env file: {ex.Message}");
        }
        //Console.WriteLine(EnvValues.Token.ToString());

        //Console.WriteLine($"{EnvValues.RedisHost}:{EnvValues.RedisPort},password={EnvValues.RedisPassword}");
        var userDb = new RedisServices("localhost:6379",false);
        await userDb.StartConnection();
        var MongoClient = new MongoClient("localhost:27017");
        var MongoDatabase = MongoClient.GetDatabase("RumbaBot");
        
        var UserRepo = new UserProfileRepositeory(MongoDatabase);
        var GuildRepo = new GuildProfileRepositeory(MongoDatabase);
        
        
        DiscordClientBuilder builder = DiscordClientBuilder.CreateDefault(EnvValues.Token, DiscordIntents.All);
        
        

        builder
            .ConfigureEventHandlers
            (
                b => b
                        
                    .HandleGuildDownloadCompleted(async (s, e) =>
                    {
                        //need to check the db if the guild is already in the database and if so make sure that the data is synced
                        //
                        var guild = e.Guilds;
                        //s.Guilds.Values
                        
                        foreach (var g in guild)
                        {
                            try
                            {
                                if (await GuildRepo.CheckGuildProfile(g.Key))
                                {
                                    break;
                                }
                                else
                                {
                                    var tempGuildProfile = new GuildProfile()
                                    {
                                        GuildId = g.Key,
                                        Name = g.Value.Name,
                                        //add more when I add more settings
                                    };
                                    await GuildRepo.CreateGuildProfile(tempGuildProfile);
                                }
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception);
                                throw;
                            }
                        }
                    })
                    
                    .HandleMessageUpdated(async (e, s) =>
                    {
                        string before;
                        Tuple<ulong,ulong> logSettings;
                        ulong logChennal = 0;
                        
                        //CHECK CHECHE
                        try
                        {
                            logSettings = await userDb.GetGuildLogSettingInfo(s.Guild.Id);
                            var x = logSettings;
                            if (logSettings.Item2 == 0)
                            {
                                //need to finish this part
                            }
                            
                            logChennal = logSettings.Item2;
                        }
                        catch (Exception exception)
                        {
                            await userDb.CacheGuildInfo(s.Guild.Id, );
                            logChennal = 0;
                        }
                        
                        
                        //Get Org message
                        try
                        {
                            before = s.MessageBefore.ToString();
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                            before = "message not ceched";
                        }
                        
                        
                        string after = s.Message.ToString();


                        var embed = new DiscordEmbedBuilder()
                            {
                                Title = "Message edited",
                                
                            }
                            .AddField("UserName", s.Author.Username, true)
                            .AddField("Orginal Message", $"Original message: {before}");


                        var chennal = await s.Guild.GetChannelAsync(logChennal);
                        await chennal.SendMessageAsync(embed: embed);


                    })
                    
                    .HandleMessageDeleted(async (e, s) =>
                    {
                        
                    })
                    
                    
                    .HandleMessageCreated(async (s, e) =>
                    {
                        if (e.Author.IsBot == true) return;
                        
                        //get the user string
                        //
                        //
                        //to the xp and other data point from the dbs
                        string userKey = $"{e.Author.Id}-{e.Author.GlobalName}-{e.Guild.Id}";
                        //todo load quick user data from redis
                        //todo do any need checks for data
                        
                        //todo 
                        
                        if (e.Message.Content.ToLower().StartsWith("?hello"))
                        {
                            await e.Message.RespondAsync($"Hello, {e.Message.Author.Username}!");
                        }

                        if (e.Message.Content.ToLower().StartsWith("?help"))
                        {
                            await e.Message.RespondAsync($"Sorry, {e.Message.Author.Username}! This section is still under construction.");
                        }

                        if (e.Message.Content.ToLower().StartsWith("!save"))
                        {
                            
                            Console.WriteLine(userKey);
                            
                            var userMessage = InputParser.TryParseCommand("!","save", e.Message.Content);
                            if (userMessage != "")
                                try
                                {
                                    await userDb.SaveMessage(userKey, userMessage);
                                    await e.Message.RespondAsync($"Saved Successfully!");
                                }
                                catch (Exception exception)
                                {
                                    Console.WriteLine(exception);
                                    await e.Message.RespondAsync($"Failed to save message!");
                                    throw;
                                }
                            else
                            {
                                
                            }
                        }

                        if (e.Message.Content.ToLower().StartsWith("!retrieve"))
                        {
                            //userKey = $"{e.Author.Id}-{e.Author.GlobalName}-{e.Guild.Id}";

                            try
                            {
                                string result = await userDb.RetrieveMessage(userKey);
                                await e.Message.RespondAsync(result);
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception);
                                throw;
                            }
                            
                        }
                    })
            );
        
        Client = builder.Build();
        
        await Client.ConnectAsync();
        await Task.Delay(-1);
    }
}