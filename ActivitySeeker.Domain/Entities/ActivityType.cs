using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivitySeeker.Domain.Entities;

[Table("activity_type", Schema = "activity_seeker")]
public class ActivityType
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("type_name")]
    public string TypeName { get; set; } = default!;

    #region Navigation properties

    public IEnumerable<Activity>? Activities { get; set; }

    #endregion
}