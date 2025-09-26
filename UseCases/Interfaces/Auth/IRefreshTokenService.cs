using Domain.Entities.Account;

namespace UseCases.Interfaces.Auth
{
    internal interface IRefreshTokenService
    {
        Task<(string accessToken, string refreshToken)> IssueAsync(Account account, string? ip, CancellationToken ct);
        Task<(string accessToken, string refreshToken)> RefreshAsync(string refreshToken, string? ip, CancellationToken ct);
        Task RevokeAsync(string refreshToken, string? ip, string reason, CancellationToken ct);
    }
}
