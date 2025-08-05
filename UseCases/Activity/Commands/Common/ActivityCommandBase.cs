
namespace UseCases.Activity.Commands.Common
{
    public class ActivityCommandBase
    {
        public Guid ActivityTypeId { get; init; }
        public string Description { get; init; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Timezone { get; init; }
        public bool IsOnline { get; init; }
        public int? CityId { get; init; }
    }
}
