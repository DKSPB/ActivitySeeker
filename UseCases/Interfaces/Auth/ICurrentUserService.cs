namespace UseCases.Interfaces.Auth;

public interface ICurrentUserService
{
    long? UserId { get; }

    bool IsAdmin { get; }
}