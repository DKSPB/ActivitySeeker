using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain;
using ActivitySeeker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ActivitySeeker.Bll.Services;

public class AdminService: IAdminService
{
    private readonly ActivitySeekerContext _activitySeekerContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    
    public AdminService(ActivitySeekerContext activitySeekerContext, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _activitySeekerContext = activitySeekerContext;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }
    public async Task RegisterAsync(string userName, string password)
    {
        var userExists = await _activitySeekerContext.Admins.FirstOrDefaultAsync(x => x.Username == userName);

        if (userExists is not null)
        {
            throw new ArgumentException("Пользователь с таким именем уже существует");
        }

        var hashedPassword = _passwordHasher.Generate(password);

        await _activitySeekerContext.Admins.AddAsync(new Admin
        {
            Username = userName,
            HashedPassword = hashedPassword
        });

        await _activitySeekerContext.SaveChangesAsync();
    }

    public async Task<string> LoginAsync(string userName, string password)
    {
        var userExists = await _activitySeekerContext.Admins.FirstOrDefaultAsync(x => x.Username == userName);

        if (userExists is null)
        {
            throw new Exception("Неверный логин и/или пароль");
        }

        var resulVerify = _passwordHasher.Verify(password, userExists.HashedPassword);

        if (!resulVerify)
        {
            throw new Exception("Неверный логин и/или пароль");
        }

        var token = _jwtProvider.GenerateToken(userExists);

        return token;
    }
}