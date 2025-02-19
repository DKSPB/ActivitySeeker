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
    public class StateEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; init; } = default!;

        public ICollection<TransitionEntity> OutgoingTransitions { get; set; } = new List<TransitionEntity>();

        public ICollection<TransitionEntity> IncomingTransitions { get; set; } = new List<TransitionEntity>();
    }
}
