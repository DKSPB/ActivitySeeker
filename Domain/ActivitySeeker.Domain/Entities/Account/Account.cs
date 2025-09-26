using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Account
{
    [Table("account", Schema = "identity")]
    public class Account
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("display_name")]
        public string DisplayName { get; set; } = string.Empty;

        [Column("phone")]
        public string Phone { get; set; } = string.Empty;

        [Column("mail")]
        public string Mail { get;set; } = string.Empty;

        [Column("account_type")]
        public AccountType AccountType { get; set; }

        [Column("role")]
        public Role Role { get; set; }

        #region Навигационные свойства
        public IQueryable<ExternalLogin> Logins { get; set; } = default!;
        #endregion
    }
}
