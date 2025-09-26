using System.Text.RegularExpressions;

namespace Rumba_Bot.Services.StringUtility;

public class UserInputProcessor
{
    
    public UserInputProcessor()
    {
        
    }

    public string TryParseCommand(string prefix, string keyword, string message)
    {
        string pattern = $@"(?<={Regex.Escape(prefix)}{Regex.Escape(keyword)}\s)";
        Regex commandWordParser =  new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase, TimeSpan.FromSeconds(3));
        try
        {
            var results = commandWordParser.Split(message, 2);
            Console.WriteLine(results[1]);
            return results[1];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "";
        }
        
    }
}