using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivitySeeker.Domain.Entities;

[Table("activity", Schema = "activity_seeker")]
public class Activity
{
    [Key]
    public Guid Id { get; set; }

    [Column("description")]
    public string? Description { get; set; }
    
    [Column("link")] 
    public string? Link { get; set; }
    
    [Column("start_date")]
    public DateTime StartDate { get; set; }
    
    [Column("activity_type_id")]
    public Guid ActivityTypeId { get; set; }

    #region Navigation properties

    public ActivityType ActivityType { get; set; } = null!;
    
    public ICollection<Image>? Images { get; set; }

    #endregion
}