using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivitySeeker.Domain.Entities
{
    [Table("state", Schema = "definition")]
    public class BotState
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = default!;

        public ICollection<BotTransition>? Transitions { get; set; }
    }
}
