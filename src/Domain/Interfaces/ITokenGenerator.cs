using StudentCenterAuthApi.src.Application.DTOs;

namespace StudentCenterAuthApi.src.Domain.Interfaces;

public interface ITokenGenerator
{
    string GenerateToken(UserDataLoginDto userDto);
}
