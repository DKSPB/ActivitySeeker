
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Account
{
    [Table("refresh_token", Schema = "identity")]
    public class RefreshToken
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("account_id")]
        public Guid AccountId { get; set; }

        [Column("token_hash")]
        public string TokenHash { get; set; } = string.Empty;

        [Column("expires_at_utc")]
        public DateTime ExpiresAtUtc { get; set; }

        [Column("created_at_utc")]
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        [Column("created_by_ip")]
        public string? CreatedByIp { get; set; }

        [Column("revoked_at_utc")]
        public DateTime? RevokedAtUtc { get; set; }
        
        [Column("revoked_by_ip")]
        public string? RevokedByIp { get; set; }

        [Column("replaced_by_token_hash")]
        public string? ReplacedByTokenHash { get; set; }

        [Column("reason_revoked")]
        public string? ReasonRevoked { get; set; }

        public bool IsExpired()
        {
            return DateTime.UtcNow >= ExpiresAtUtc;
        }

        public bool IsActive() 
        {
            return RevokedAtUtc is null && !IsExpired(); 
        }

        #region Навигационные свойства
        public Account Account { get; set; } = default!;
        #endregion
    }
}
