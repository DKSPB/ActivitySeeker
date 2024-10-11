using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ActivitySeeker.Bll.Utils;

public class JwtProvider: IJwtProvider
{
    private readonly JwtOptions _jwtOptions;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _jwtOptions = options.Value;
    }

    public string GenerateToken(Admin admin)
    {
        Claim userIdClaim = new ("adminId", admin.Id.ToString());
        Claim usernameClaim = new("username", admin.Login);
        List<Claim> claims = new() { userIdClaim, usernameClaim};

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), 
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours));
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}