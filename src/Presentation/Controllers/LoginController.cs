using Microsoft.AspNetCore.Mvc;
using StudentCenterAuthApi.src.Application.DTOs;
using StudentCenterAuthApi.src.Application.Interfaces;
using StudentCenterAuthApi.src.Domain.Interfaces;

namespace StudentCenterAuthApi.src.Presentation.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LoginController : Controller
{
    private readonly IUserService _userService;
    private readonly ITokenGenerator _tokenGenerator;

    public LoginController(IUserService userService, ITokenGenerator tokenGenerator)
    {
        _userService = userService;
        _tokenGenerator = tokenGenerator;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var user = await _userService.UserLogin(userLoginDto.Email, userLoginDto.PassWord);

        if (user == null) return Unauthorized();

        var token = _tokenGenerator.GenerateToken(user);

        return Ok(new { token });
    }
}
