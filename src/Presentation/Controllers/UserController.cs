using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCenterAuthApi.src.Application.DTOs;
using StudentCenterAuthApi.src.Application.Interfaces;

namespace StudentCenterAuthApi.src.Presentation.Controllers;

/// <summary>
/// Controller responsible for handling user-related operations via RESTful endpoints
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class UserController : BaseController
{
    private readonly IUserService _service;
    private readonly IRabbitMQService _rabbitMQService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserController"/> class
    /// </summary>
    /// <param name="service">Service for managing user operations</param>
    /// <param name="rabbitMQService">Service for messaging with RabbitMQ</param>
    public UserController(IUserService service, 
                          IRabbitMQService rabbitMQService)
    {
        _service = service;
        _rabbitMQService = rabbitMQService;
    }

    /// <summary>
    /// Retrieves login information for a user based on credentials
    /// </summary>
    /// <param name="Email">User's email address</param>
    /// <param name="PassWord">User's password</param>
    /// <returns>User login data or unauthorized result</returns>
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

    /// <summary>
    /// Retrieves all users in the system
    /// </summary>
    /// <returns>A list of users</returns>
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

    /// <summary>
    /// Creates a new user with the provided data
    /// </summary>
    /// <param name="userDto">User information for creation</param>
    /// <returns>Status result indicating success or failure</returns>
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

    /// <summary>
    /// Deletes a user by their email address
    /// </summary>
    /// <param name="email">Email of the user to delete</param>
    /// <returns>Status result indicating success or failure</returns>
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

    /// <summary>
    /// Updates the user's name and email based on their ID
    /// </summary>
    /// <param name="id">Unique identifier of the user</param>
    /// <param name="userUpdateDto">New name and email data</param>
    /// <returns>Status result with the updated user information</returns>
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

    /// <summary>
    /// Updates a user's password based on their ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="passWord">New password to be set</param>
    /// <returns>Status result with login data or error</returns>
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
