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
    
    [Column("message_id")]
    public int MessageId { get; set; }

    [Column("username")]
    public string UserName { get; set; } = default!;

    [Column("state")]
    public string State { get; set; } = default!;
}