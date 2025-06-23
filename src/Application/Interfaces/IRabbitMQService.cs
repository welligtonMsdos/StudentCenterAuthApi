using StudentCenterAuthApi.src.Application.DTOs;

namespace StudentCenterAuthApi.src.Application.Interfaces;

public interface IRabbitMQService
{
    Task<bool> PublishMessage(UserDto userDto);
}
