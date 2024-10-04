using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivitySeeker.Domain.Entities;

[Table("admin", Schema = "activity_seeker")]
public class Admin
{
    [Key]
    public Guid Id { get; set; }

    [Column("username")] 
    public string Username { get; set; } = default!;

    [Column("hashed_password")]
    public string HashedPassword { get; set; } = default!;

}