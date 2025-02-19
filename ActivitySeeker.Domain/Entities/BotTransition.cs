using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivitySeeker.Domain.Entities
{
    [Table("transition", Schema = "definition")]
    public class BotTransition
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("from_state_id")]
        public int FromStateId { get; set; }

        [Column("to_state_id")]
        public int ToStateId { get; set; }

        public BotState FromState { get; set; } = default!;

        public BotState ToState { get; set; } = default!;
    }
}
