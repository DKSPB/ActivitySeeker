using UseCases.Interfaces;

namespace UseCases.Common;

public class DateTimeConverter : IDateTimeConverter
{
    /// <summary>
    /// Конвертирует локальное время пользователя в формат UTC
    /// </summary>
    /// <param name="localTime">Время пользователя</param>
    /// <param name="offsetHours">Смещение времени (из VK-api)</param>
    /// <returns>Время пользователя в формате UTC</returns>
    public DateTime? ToUtc(DateTime? localTime, int offsetHours)
    {
        if (localTime is null)
        {
            return null;
        }
        
        var unspecified = DateTime.SpecifyKind(localTime.Value, DateTimeKind.Unspecified);
            
        var offset = TimeSpan.FromHours(offsetHours);
        
        return DateTime.SpecifyKind(unspecified - offset, DateTimeKind.Utc);
    }
    
    /// <summary>
    /// Конвертирует время из формата UTC в локальное время пользователя
    /// </summary>
    /// <param name="utcTime">Время в формате UTC</param>
    /// <param name="offsetHours">Смещение времени (из VK-api)</param>
    /// <returns>Локальное время пользователя</returns>
    public DateTime? ToLocal(DateTime? utcTime, int offsetHours)
    {
        if (utcTime is null)
        {
            return null;
        }
        
        var offset = TimeSpan.FromHours(offsetHours);
        
        return DateTime.SpecifyKind(utcTime.Value + offset, DateTimeKind.Unspecified);
    }
}