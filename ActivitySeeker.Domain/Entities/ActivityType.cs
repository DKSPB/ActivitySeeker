using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivitySeeker.Domain.Entities;

[Table("activity_type", Schema = "activity_seeker")]
public class ActivityType
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("parent_id")]
    public Guid? ParentId { get; set; }

    [Column("type_name")]
    public string TypeName { get; set; } = default!;

    [Column("image_path")]
    public string? ImagePath { get; set; }

    #region Navigation properties

    public IEnumerable<Activity>? Activities { get; set; }
    
    public List<ActivityType>? Children { get; set; }
    
    public ActivityType? Parent { get; set; }

    #endregion
}