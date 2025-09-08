// See https://aka.ms/new-console-template for more information



namespace Rumba_Bot;



class DiscordBot
{
    
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
        DiscordClientBuilder builder = DiscordClientBuilder.CreateDefault(EnvValues.Token, DiscordIntents.All);
        

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
                    })
            );
        
        Client = builder.Build();
        
        await Client.ConnectAsync();
        await Task.Delay(-1);
    }
}