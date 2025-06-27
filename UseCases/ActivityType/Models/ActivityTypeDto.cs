namespace UseCases.ActivityType.Models
{
    public class ActivityTypeDto
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
    }
}
