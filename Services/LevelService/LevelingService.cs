namespace Rumba_Bot.Services.LevelService;

public class LevelingService
{
    private string XpCurve { get; set; }

    public LevelingService(var GuildXpCurve)
    {
        try
        {

        }
        catch (Exception e)
        {
            //set to the default curve
        }

    }

    public LevelingService()
    {
        XpCurve = "EXP";
    }
    
    
}