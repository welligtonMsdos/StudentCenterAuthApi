using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCenterAuthApi.src.Application.DTOs;
using StudentCenterAuthApi.src.Application.Interfaces;

namespace StudentCenterAuthApi.src.Presentation.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : BaseController
{
    private readonly IUserService _service;
    private readonly IRabbitMQService _rabbitMQService;

    public UserController(IUserService service, 
                          IRabbitMQService rabbitMQService)
    {
        _service = service;
        _rabbitMQService = rabbitMQService;
    }

    [Authorize]
    [HttpGet("[Action]")]
    public async Task<IActionResult> GetUserLogin(string Email, string PassWord)
    {
        try
        {
            return Ok(await _service.AuthenticateUser(Email, PassWord));
        }
        catch (Exception ex)
        {
            return Error(ex);
        }
    }

    [Authorize]
    [HttpGet("[Action]")]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            return Ok(await _service.GetAllUsers());
        }
        catch (Exception ex)
        {
            return Error(ex);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] UserCreateDto userDto)
    {
        try
        {
            var newUser = await _service.AddNewUser(userDto);

            var resultRabbitMQ = _rabbitMQService.PublishMessage(newUser);

            return Sucess(newUser);
        }
        catch (Exception ex)
        {
            return Error(ex);
        }
    }

    [Authorize]
    [HttpDelete("{email}")]
    public async Task<ActionResult> Delete(string email)
    {
        try
        {
            return Sucess(await _service.DeleteByEmail(email));
        }
        catch (Exception ex)
        {
            return Error(ex);
        }
    }

    [Authorize]
    [HttpPut("[Action]/{id:length(24)}")]
    public async Task<IActionResult> UpdateUserNameAndEmail(string id, [FromBody] UserUpdateDto userUpdateDto)
    {
        try
        {
            return Sucess(await _service.UpdateNameAndEmail(id, userUpdateDto));
        }
        catch (Exception ex)
        {
            return Error(ex);
        }
    }

    [Authorize]
    [HttpPut("[Action]/{id:length(24)}")]
    public async Task<IActionResult> UpdatePassword(string id, string passWord)
    {
        try
        {
            return Sucess(await _service.UpdatePassword(id, passWord));
        }
        catch (Exception ex)
        {
            return Error(ex);
        }
    }
}
