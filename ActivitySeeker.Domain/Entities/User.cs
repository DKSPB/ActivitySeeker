using Npgsql.PostgresTypes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ActivitySeeker.Domain.Entities;

[Table("user", Schema = "activity_seeker")]
public class User
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("chat_id")]
    public string ChatId { get; set; } = default!;

    [Column("username")]
    public string UserName { get; set; } = default!;

    [Column("state", TypeName = "jsonb")]
    public string State { get; set; } = default!;
}