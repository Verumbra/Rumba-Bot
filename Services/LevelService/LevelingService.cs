namespace Rumba_Bot.Services.LevelService;

public class LevelingService
{
    private string XpCurve { get; set; }

    public LevelingService(string xpCurve)
    {
        try
        {
            //load stuff from cache or database
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