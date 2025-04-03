namespace StudentCenterAuthApi.src.Application.DTOs;

public record UserDataLoginDto(string _id,
                               string Name,
                               string Email,
                               string PassWord,
                               bool FirstAccess);

