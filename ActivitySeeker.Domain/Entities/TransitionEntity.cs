using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivitySeeker.Domain.Entities
{
    [Table("transition", Schema = "definition")]
    public class TransitionEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("from_state_id")]
        public int FromStateId { get; init; }

        [Column("to_state_id")]
        public int ToStateId { get; init; }

        [Column("name")]
        public string Name { get; init; } = default!;

        public StateEntity FromState { get; set; } = default!;

        public StateEntity ToState { get; set; } = default!;
    }
}
