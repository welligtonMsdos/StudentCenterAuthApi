using StudentCenterAuthApi.src.Application.DTOs;

namespace StudentCenterAuthApi.src.Application.Interfaces;

public interface IUserService
{
    Task<ICollection<UserDto>> GetAllUsers();
    Task<UserDto> AddNewUser(UserCreateDto user);
    Task<bool> DeleteByEmail(string email);
    Task<UserDto> UpdateNameAndEmail(string id, UserUpdateDto user);
}
