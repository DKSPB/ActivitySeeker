using System.ComponentModel.DataAnnotations.Schema;

namespace ActivitySeeker.Domain.Entities;

[Table("image", Schema = "activity_seeker")]
public class Image
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("activity_id")]
    public Guid ActivityId { get; set; }

    [Column("content")] 
    public byte[] Content { get; set; } = null!;

    #region Navigation properties

    public Activity Activity { get; set; } = null!;

    #endregion
}