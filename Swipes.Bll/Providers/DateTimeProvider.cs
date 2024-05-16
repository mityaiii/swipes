namespace Swipes.Bll.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset Now()
    {
        return DateTimeOffset.Now;
    }
}