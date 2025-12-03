namespace Rumba_Bot.Services.ConversionUtility;

public class UlongProcessor
{
    public int UlongtoInt(ulong value)
    {
        int result = unchecked((int)value);
        return result;
    }

    public ulong InttoUlong(int value)
    {
        return (ulong)value;
    }
    
}