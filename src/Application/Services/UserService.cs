using AutoMapper;
using FluentValidation.Results;
using StudentCenterAuthApi.src.Application.DTOs;
using StudentCenterAuthApi.src.Application.Interfaces;
using StudentCenterAuthApi.src.Domain.Interfaces;
using StudentCenterAuthApi.src.Domain.Model;
using StudentCenterAuthApi.src.Domain.Validation;


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
        var userToUpdate = new User
        {
            Email = email
        };

        ValidationResult valid = new UserEmailValidation().Validate(userToUpdate);

        string[] erros = valid.ToString("~").Split('~');

        if (!valid.IsValid) throw new Exception(erros[0]);

        return await _repository.DeleteByEmail(email);
    }

    public async Task<ICollection<UserDto>> GetAllUsers()
    {
        return _mapper.Map<ICollection<UserDto>>(await _repository.GetAllUsers());
    }   

    public async Task<UserDto> AddNewUser(UserCreateDto user)
    {
        var newUser = _mapper.Map<User>(user);

        ValidationResult valid = new UserNameEmailValidation().Validate(newUser);

        string[] erros = valid.ToString("~").Split('~');

        if (!valid.IsValid) throw new Exception(erros[0]);

        newUser.Active = true;

        newUser.LastAccess = DateTime.Now;

        newUser.FirstAccess = true;

        newUser.PassWord = BCrypt.Net.BCrypt.HashPassword(GeneratePassWord(user.Name));

        return _mapper.Map<UserDto>(await _repository.AddNewUser(newUser));
    }

    public async Task<UserDto> UpdateNameAndEmail(string id, UserUpdateDto user)
    {
        var userToUpdate = _mapper.Map<User>(user);

        ValidationResult valid = new UserNameEmailValidation().Validate(userToUpdate);

        string[] erros = valid.ToString("~").Split('~');

        if (!valid.IsValid) throw new Exception(erros[0]);

        return _mapper.Map<UserDto>(await _repository.UpdateNameAndEmail(id, userToUpdate));
    }

    private string GeneratePassWord(string name)
    {
        var passWord = string.Concat(name.Split(' ').Select(palavra => char.ToLower(palavra[0])));

        passWord += DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day;

        return passWord;
    }

    public async Task<UserDataLoginDto> UpdatePassword(string id, string passWord)
    {
        var userToUpdate = new User
        {
            PassWord = passWord
        };

        ValidationResult valid = new UserPasswordValidation().Validate(userToUpdate);

        string[] erros = valid.ToString("~").Split('~');

        if (!valid.IsValid) throw new Exception(erros[0]);

        var newPassword = BCrypt.Net.BCrypt.HashPassword(passWord);  

        return _mapper.Map<UserDataLoginDto>(await _repository.UpdatePassword(id, newPassword));
    }

    public async Task<UserDataLoginDto> AuthenticateUser(string Email, string PassWord)
    {
        var userLoginDto = new UserLoginDto(Email, PassWord);

        var user = _mapper.Map<User>(userLoginDto);

        ValidationResult valid = new UserValidation().Validate(user);

        string[] erros = valid.ToString("~").Split('~');

        if (!valid.IsValid) throw new Exception(erros[0]);

        var userLogged = await _repository.AuthenticateUser(user);

        return _mapper.Map<UserDataLoginDto>(userLogged);
    }

    public async Task<UserDataLoginDto> UpdateLastAccess(string Email, string PassWord)
    {
        var userLoginDto = new UserLoginDto(Email, PassWord);

        var user = _mapper.Map<User>(userLoginDto);

        user.LastAccess = DateTime.Now;

        user.FirstAccess = false;

        ValidationResult valid = new UserValidation().Validate(user);

        string[] erros = valid.ToString("~").Split('~');

        if (!valid.IsValid) throw new Exception(erros[0]);

        var userLogged = await _repository.UpdateLastAccess(user);

        return _mapper.Map<UserDataLoginDto>(userLogged);
    }
}
