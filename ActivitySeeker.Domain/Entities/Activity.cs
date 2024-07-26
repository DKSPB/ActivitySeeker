using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivitySeeker.Domain.Entities;

[Table("activity", Schema = "activity_seeker")]
public class Activity
{
    [Key]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = default!;

    [Column("description")]
    public string Description { get; set; } = default!;

    [Column("link")] 
    public string? Link { get; set; }
    
    [Column("start_date")]
    public DateTime StartDate { get; set; }
    
    [Column("activity_type_id")]
    public Guid ActivityTypeId { get; set; }

    #region Навигационные свойства

    public ActivityType ActivityType { get; set; } = null!;

    #endregion
}