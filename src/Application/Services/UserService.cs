using AutoMapper;
using StudentCenterAuthApi.src.Application.DTOs;
using StudentCenterAuthApi.src.Application.Interfaces;
using StudentCenterAuthApi.src.Domain.Interfaces;
using StudentCenterAuthApi.src.Domain.Model;


namespace StudentCenterAuthApi.src.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> DeleteByEmail(string email)
    {
        return await _repository.DeleteByEmail(email);
    }

    public async Task<ICollection<UserDto>> GetAllUsers()
    {
        return _mapper.Map<ICollection<UserDto>>(await _repository.GetAllUsers());
    }

    public async Task<UserDto> AddNewUser(UserCreateDto user)
    {
        var newUser = _mapper.Map<User>(user);

        newUser.Active = true;
        newUser.LastAccess = DateTime.Now;
        newUser.FirstAccess = false;
        newUser.PassWord = BCrypt.Net.BCrypt.HashPassword(GeneratePassWord(user.Name));

        return _mapper.Map<UserDto>(await _repository.AddNewUser(newUser));
    }

    public async Task<UserDto> UpdateNameAndEmail(string id, UserUpdateDto user)
    {
        var userToUpdate = _mapper.Map<User>(user);

        return _mapper.Map<UserDto>(await _repository.UpdateNameAndEmail(id, userToUpdate));
    }

    private string GeneratePassWord(string name)
    {
        var passWord = string.Concat(name.Split(' ').Select(palavra => char.ToLower(palavra[0])));

        passWord += DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day;

        return passWord;
    }   
}
