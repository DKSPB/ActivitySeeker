using System.ComponentModel.DataAnnotations.Schema;

namespace ActivitySeeker.Domain.Entities;

[Table("user", Schema = "activity_seeker")]
public class User
{
    [Column("id")]
    public long Id { get; set; }

    [Column("chat_id")]
    public long ChatId { get; set; }
    
    [Column("state_id")]
    public Guid StateId { get; set; }

    [Column("username")]
    public string UserName { get; set; } = default!;

    [Column("is_admin")]
    public bool IsAdmin { get; set; } = false;
    
    [Column("activity_result")]
    public string ActivityResult { get; set; } = default!;

    #region Навигационные свойства

    public State State { get; set; } = new();

    #endregion
}