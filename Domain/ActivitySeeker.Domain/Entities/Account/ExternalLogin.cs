using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Account
{
    [Table("external_login", Schema = "identity")]
    public class ExternalLogin
    {
        [Column("id")]
        public  Guid Id { get; set; }

        [Column("account_id")]
        public Guid AccountId { get; set; }

        [Column("auth_provider")]
        public AuthProvider AuthProvider { get; set; }

        [Column("provider_key")]
        public string ProviderKey { get; set; } = string.Empty!;

        [Column("extra_data")]
        public string ExtraData { get; set; } = string.Empty;

        #region Навигационные свойства
        public Account Account { get; set; } = default!;
        #endregion
    }
}