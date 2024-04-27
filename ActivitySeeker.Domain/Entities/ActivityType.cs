using System.ComponentModel.DataAnnotations.Schema;

namespace ActivitySeeker.Domain.Entities;

[Table("activity_type", Schema = "activity_seeker")]
public class ActivityType
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("type_value")] 
    public string TypeValue { get; set; } = default!;

    [Column("type_name")]
    public string TypeName { get; set; } = default!;
}