using System.Globalization;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public static class DateParser
{
    private const string DateFormat = "dd.MM.yyyy";

    private const string DateTimeFormat = "dd.MM.yyyy HH:mm";
    
    public static bool ParseDate(string fromDateText, out DateTime fromDate)
    {
        return DateTime.TryParseExact(fromDateText, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate);
    }
    
    public static bool ParseDateTime(string fromDateText, out DateTime fromDate)
    {
        return DateTime.TryParseExact(fromDateText, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate);
    }
}