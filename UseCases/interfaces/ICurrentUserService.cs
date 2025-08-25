namespace UseCases.Interfaces;

public interface ICurrentUserService
{
    long? UserId { get; }

    bool IsAdmin { get; }
}