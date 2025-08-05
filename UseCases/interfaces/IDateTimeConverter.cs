namespace UseCases.Interfaces;

public interface IDateTimeConverter
{
    DateTime? ToUtc(DateTime? localTime, int timezone);
    
    DateTime? ToLocal(DateTime? utcTime, int timezone);
}