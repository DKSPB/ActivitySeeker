using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivitySeeker.Domain.Entities;

[Table("admin", Schema = "activity_seeker")]
public class Admin
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("login")] 
    public string Login { get; set; } = default!;

    [Column("hashed_password")]
    public string HashedPassword { get; set; } = default!;
}