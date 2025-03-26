using AutoMapper;
using StudentCenterAuthApi.src.Application.DTOs;
using StudentCenterAuthApi.src.Application.Interfaces;
using StudentCenterAuthApi.src.Domain.Interfaces;

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

    public async Task<ICollection<UserDto>> GetAll()
    {
        return _mapper.Map<ICollection<UserDto>>(await _repository.GetAll());
    }
}
