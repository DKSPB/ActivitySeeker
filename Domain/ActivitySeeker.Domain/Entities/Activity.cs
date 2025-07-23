using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("activity", Schema = "activity_seeker")]
    public class Activity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("activity_type_id")]
        public Guid ActivityTypeId { get; set; }

        [Column("description")]
        public string Description { get; set; } = null!;

        [Column("start_date")]
        public DateTime StartDate { get; set; }
        
        [Column("end_date")]
        public DateTime? EndDate { get; set; }

        [Column("image")]
        public byte[]? Image { get; set; }

        [Column("city_id")]
        public int? CityId { get; set; }

        [Column("is_online")]
        public bool IsOnline { get; set; }

        [Column("is_published")]
        public bool? IsPublished { get; set; }

        #region Навигационные свойства

        public ActivityType ActivityType { get; set; } = null!;

        public User Author { get; set; } = null!;

        //public City? ActivityCity { get; set; }

        #endregion
    }
}
