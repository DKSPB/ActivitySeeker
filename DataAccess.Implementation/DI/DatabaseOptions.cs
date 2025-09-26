namespace DataAccess.DI;
internal class DatabaseOptions
{
    public const string SectionName = "ConnectionStrings";
    public string ActivitySeekerConnection { get; set; } = string.Empty;
}