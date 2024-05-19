using Npgsql.PostgresTypes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ActivitySeeker.Domain.Entities;

[Table("user", Schema = "activity_seeker")]
public class User
{
    [Column("id")]
    public long Id { get; set; }

    [Column("chat_id")]
    public long ChatId { get; set; }

    [Column("username")]
    public string UserName { get; set; } = default!;

    [Column("message_id")]
    public int MessageId { get; set; }

    [Column("activity_type_id")]
    public Guid ActivityTypeId { get; set; }

    [Column("search_from")]
    public DateTime SearchFrom { get; set; }

    [Column("search_to")]
    public DateTime SearchTo { get; set; }
}