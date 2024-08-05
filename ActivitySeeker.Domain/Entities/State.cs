using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivitySeeker.Domain.Entities;

[Table("state", Schema = "activity_seeker")]
public class State
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [Column("state")]
    public StatesEnum StateNumber { get; set; }
    
    [Column("message_id")]
    public int MessageId { get; set; }
    
    [Column("activity_type_id")]
    public Guid? ActivityTypeId { get; set; }
    
    [Column("search_from")]
    public DateTime SearchFrom { get; set; }
    
    [Column("search_to")]
    public DateTime SearchTo { get; set; }

    #region NavigationProperties

    public ActivityType? ActivityType { get; set; }

    public User User { get; set; }

    #endregion
}