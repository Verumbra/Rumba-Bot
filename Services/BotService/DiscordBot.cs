using DSharpPlus.EventArgs;
using Rumba_Bot.Services.RedisService;

namespace Rumba_Bot.Services.BotService;

public class RumbaConfig
{
    public readonly EnvReader EnvValues = new EnvReader();
    public bool isProd {get; set;}
    
    
}

public class GuildManagementHelper
{
    public async Task OnAllGuildsReady(DiscordClient client, GuildDownloadCompletedEventArgs)
    {
        
    }
    private async Task<[UserProfile]> ProcessGuildMembers(DiscordGuild guild)
    
}

public class DiscordBot
{
    private DiscordClient RumbaClient {get; set;}
    private RedisServices QuickDb {get; set;}

    DiscordBot(RumbaConfig options)
    {
        QuickDb = new RedisServices($"{options.EnvValues.RedisHost}:{options.EnvValues.RedisPort},password={options.EnvValues.RedisPassword}", options.isProd);
        
        DiscordClientBuilder builder = DiscordClientBuilder.CreateDefault(options.EnvValues.Token, DiscordIntents.All);
        

        builder.ConfigureEventHandlers
        (
            b => b
                    
                    
                .HandleMessageCreated(async (s, e) =>
                {
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
                        string userKey = e.Author.Id.ToString() + "-" + e.Author.GlobalName + "-" + e.Guild.Id.ToString();

                        try
                        {
                            await QuickDb.SaveMessage(userKey, e.Message.Content);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                            throw;
                        }
                    }

                    if (e.Message.Content.ToLower().StartsWith("!retrieve"))
                    {
                            
                    }
                })
            
            
        );
        RumbaClient = builder.Build();
    }

    async public Task InitizeConnect()
    {
        
    }
    
    
}