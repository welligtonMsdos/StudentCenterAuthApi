using Microsoft.IdentityModel.Tokens;
using StudentCenterAuthApi.src.Application.DTOs;
using StudentCenterAuthApi.src.Domain.Interfaces;
using StudentCenterAuthApi.src.Infrastructure.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentCenterAuthApi.src.Infrastructure.Data.Authentication;

public class TokenGenenator : ITokenGenerator
{    
    public async Task<string> GenerateToken(UserDataLoginDto userDto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var keyVault = await Util.GetSecretWithCacheAsync();
       
        var key = Encoding.ASCII.GetBytes(keyVault);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim("email", userDto.Email),
                    new Claim("id", userDto._id),
                    new Claim("name", userDto.Name),
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            //Issuer = "https://studentcenterauthapi-gna4fkhdgmbyg8cc.brazilsouth-01.azurewebsites.net",
            Issuer = "https://localhost:7048",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }    
}
