using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivitySeeker.Domain.Entities;

[Table("activity", Schema = "activity_seeker")]
public class Activity
{
    [Key]
    public Guid Id { get; set; }
    
    [Column("city_id")]
    public int? CityId { get; set; }

    [Column("is_online")]
    public bool IsOnline { get; set; }

    [Column("link_description")]
    public string LinkOrDescription { get; set; } = null!;
    
    [Column("image")]
    public byte[]? Image { get; set; }
    
    [Column("start_date")]
    public DateTime StartDate { get; set; }

    [Column("activity_type_id")]
    public Guid ActivityTypeId { get; set; }

    [Column("is_published")]
    public bool IsPublished { get; set; }

    #region Навигационные свойства

    public ActivityType ActivityType { get; set; } = null!;
    
    public City? ActivityCity { get; set; }

    #endregion
}