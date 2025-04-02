using AutoMapper;
using StudentCenterAuthApi.src.Application.DTOs;
using StudentCenterAuthApi.src.Domain.Model;

namespace StudentCenterAuthApi.src.Infrastructure.Profiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserCreateDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
    }
}
