using ActivitySeeker.DataAccess.Interfaces.Infrastructure;
using ApplicationServices.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationServices.Implementation;

public class AdminService: IAdminService
{
    private readonly IDbContext _context;
    //private readonly IPasswordHasher _passwordHasher;
    //private readonly IJwtProvider _jwtProvider;
    
    public AdminService(IDbContext context/*, IPasswordHasher passwordHasher, IJwtProvider jwtProvider*/)
    {
        _context = context;
        //_passwordHasher = passwordHasher;
        //_jwtProvider = jwtProvider;
    }
    /*public async Task RegisterAsync( string login, string password)
    {
        var adminExists = await _activitySeekerContext.Admins.FirstOrDefaultAsync(x => x.Login == login);

        if (adminExists is not null)
        {
            throw new ArgumentException("Администратор с таким логином уже существует");
        }

        var hashedPassword = _passwordHasher.Generate(password);
        
        var admin = new Admin
        {
            Login = login,
            HashedPassword = hashedPassword
        };

        await _activitySeekerContext.Admins.AddAsync(admin);

        await _activitySeekerContext.SaveChangesAsync();
    }*/

    /*public async Task<string> LoginAsync(string userName, string password)
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
    }*/

    ///<inheritdoc/>
    public async Task<IEnumerable<Admin>> GetAll()
    {
        return await _context.Admins.ToListAsync();
    }

    ///<inheritdoc/>
    public async Task<Admin> GetByLogin(string login)
    {
        return await _context.Admins.FirstOrDefaultAsync(x => x.Login == login) ?? 
               throw new NullReferenceException($"Администраторм c логином {login} не существует");
    }
}