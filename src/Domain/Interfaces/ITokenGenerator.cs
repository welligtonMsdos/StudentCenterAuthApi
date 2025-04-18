using StudentCenterAuthApi.src.Application.DTOs;

namespace StudentCenterAuthApi.src.Domain.Interfaces;

public interface ITokenGenerator
{
    Task<string> GenerateToken(UserDataLoginDto userDto);
}
