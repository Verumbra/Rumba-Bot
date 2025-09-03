// See https://aka.ms/new-console-template for more information



namespace Rumba_Bot;



class DiscordBot
{
    
    private static DiscordClient Client { get; set; }
    private static EnvReader EnvValues = new EnvReader();

    static async Task Main(string[] args)
    {
        Env.Load();
        DiscordClientBuilder builder = DiscordClientBuilder.CreateDefault(EnvValues.Token, DiscordIntents.AllUnprivileged);
        Client = builder.Build();
        
        await Client.ConnectAsync();
        await Task.Delay(-1);
    }
}