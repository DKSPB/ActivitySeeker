using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("user", Schema = "activity_seeker")]
    public class User
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        #region Навигационные свойства

        public ICollection<Activity> Activities { get; set; } = default!;

        #endregion
    }
}
