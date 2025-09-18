using System.Text.RegularExpressions;

namespace Rumba_Bot.Services.StringUtility;

public class UserInputProcessor
{
    
    private UserInputProcessor()
    {
        
    }

    public string TryParseCommand(string prefix, string keyword, string message)
    {
        string pattern = $@"(?<=\Q{prefix}{keyword}\E\s)";
        Regex commandWordParser =  new Regex(pattern, RegexOptions.Compiled, TimeSpan.FromSeconds(3));
        try
        {
            var results = commandWordParser.Split(message, 2);
            Console.WriteLine(results);
            return results[1];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "";
        }
        
    }
}