using Microsoft.IdentityModel.Tokens;
using StudentCenterAuthApi.src.Application.DTOs;
using StudentCenterAuthApi.src.Domain.Interfaces;
using StudentCenterAuthApi.src.Domain.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentCenterAuthApi.src.Infrastructure.Data.Authentication;

public class TokenGenenator : ITokenGenerator
{    
    public string GenerateToken(UserDataLoginDto userDto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes("fedaf7d8863b48e197b9287d492b708e");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim("email", userDto.Email),
                    new Claim("id", userDto._id)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = "https://localhost:7048",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }   
}
