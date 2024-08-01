using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivitySeeker.Domain.Entities;

[Table("activity_state", Schema = "activity_seeker")]
public class ActivityState
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("state")]
    public int State { get; set; }
    
    [Column("activity_type_id")]
    public Guid? ActivityTypeId { get; set; }
    
    [Column("search_from")]
    public DateTime SearchFrom { get; set; }
    
    [Column("search_to")]
    public DateTime SearchTo { get; set; }

    #region NavigationProperties

    public ActivityType? ActivityType { get; set; }

    #endregion
}