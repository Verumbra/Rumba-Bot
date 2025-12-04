using MongoDB.Bson;

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

public class IDProcessor
{
    
    public ObjectId ToObjectId(string id)
    {
        ObjectId result;
        try
        {
            result = ObjectId.Parse(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return result;
    }
}