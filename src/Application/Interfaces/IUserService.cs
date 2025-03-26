using StudentCenterAuthApi.src.Application.DTOs;

namespace StudentCenterAuthApi.src.Application.Interfaces;

public interface IUserService
{
    Task<ICollection<UserDto>> GetAll();
}
