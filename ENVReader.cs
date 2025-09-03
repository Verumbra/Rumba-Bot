namespace Rumba_Bot;

internal class EnvReader
{
    public string Token {get; set;}
    public string Prefix {get; set;}

    public EnvReader()
    {
        try
        {
            Token = Environment.GetEnvironmentVariable("TOKEN");
            Prefix = Environment.GetEnvironmentVariable("PREFIX");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}

