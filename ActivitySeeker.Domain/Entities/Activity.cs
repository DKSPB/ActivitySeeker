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
    
    [Column("image")]
    public byte[]? Image { get; set; }

    [Column("link")] 
    public string? Link { get; set; }
    
    [Column("start_date")]
    public DateTime StartDate { get; set; }

    [Column("activity_type_id")]
    public Guid ActivityTypeId { get; set; }
    
    [Column("offer_state")]
    public OffersEnum OfferState { get; set; } = OffersEnum.NotPublish;

    #region Навигационные свойства

    public ActivityType ActivityType { get; set; } = null!;

    #endregion
}