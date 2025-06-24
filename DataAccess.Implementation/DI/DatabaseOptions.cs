namespace DataAccess.DI;

internal class DatabaseOptions
{
    public const string SectionName = "ActivitySeekerConnection";
    public string ConnectionString { get; set; } = string.Empty;
}