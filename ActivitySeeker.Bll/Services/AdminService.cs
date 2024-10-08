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
    public async Task RegisterAsync(string userName, string login, string password)
    {
        var adminExists = await _activitySeekerContext.Admins.FirstOrDefaultAsync(x => x.Login == login);

        if (adminExists is not null)
        {
            throw new ArgumentException("Администратор с таким логином уже существует");
        }

        var hashedPassword = _passwordHasher.Generate(password);

        var user = await _activitySeekerContext.Users.Include(x => x.AdminProfile)
            .FirstOrDefaultAsync(x => x.UserName == userName);

        if (user is null)
        {
            throw new Exception("Нет информации о пользователе");
        }

        user.AdminProfile = new Admin
        {
            Login = login,
            HashedPassword = hashedPassword
        };

        await _activitySeekerContext.SaveChangesAsync();
    }

    public async Task<string> LoginAsync(string userName, string password)
    {
        var userExists = await _activitySeekerContext.Admins.FirstOrDefaultAsync(x => x.Login == userName);

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

    public async Task<IEnumerable<Admin>> GetAll()
    {
        return await _activitySeekerContext.Admins.Include(x => x.User).ToListAsync();
    }
}