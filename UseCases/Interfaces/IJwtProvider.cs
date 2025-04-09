using Domain.Entities;

namespace ActivitySeeker.UseCases.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(Admin admin);
}