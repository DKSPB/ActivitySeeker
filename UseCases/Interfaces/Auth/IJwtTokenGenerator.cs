using Domain.Entities.Account;

namespace UseCases.Interfaces.Auth
{
    internal interface IJwtTokenGenerator
    {
        string GenerateToken(Account account, TimeSpan lifetime);
    }
}
