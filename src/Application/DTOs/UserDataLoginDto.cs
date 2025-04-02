namespace StudentCenterAuthApi.src.Application.DTOs;

public record UserDataLoginDto(string _id,
                               string Name,
                               string Email,
                               bool FirstAccess);

