using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(Admin admin);
}