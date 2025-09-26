using Domain.Entities.Account;

namespace UseCases.Interfaces.Auth
{
    public interface IAccountService
    {
        Task<Account?> FindByProviderAsync(AuthProvider provider, string providerKey, CancellationToken ct);

        Task<Account> CreateAccountWithLoginAsync(Account account, ExternalLogin login, CancellationToken ct);

        Task<Account?> GetByIdAsync(Guid id, CancellationToken ct);
    }
}
