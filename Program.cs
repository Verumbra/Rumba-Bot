// See https://aka.ms/new-console-template for more information



namespace Rumba_Bot;



class DiscordBot
{
    private static DiscordClient Client { get; set; }

    static async Task Main(string[] args)
    {
        Env.Load();
    }
}