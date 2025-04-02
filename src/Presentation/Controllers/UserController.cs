using Microsoft.AspNetCore.Mvc;
using StudentCenterAuthApi.src.Application.DTOs;
using StudentCenterAuthApi.src.Application.Interfaces;

namespace StudentCenterAuthApi.src.Presentation.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        try
        {
            return Ok(await _service.GetAllUsers());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] UserCreateDto userDto)
    {
        try
        {
            return Ok(await _service.AddNewUser(userDto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{email}")]
    public async Task<ActionResult> Delete(string email)
    {
        try
        {
            return Ok(await _service.DeleteByEmail(email));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:length(24)}")] 
    public async Task<IActionResult> UpdateUserNameAndEmail(string id, [FromBody] UserUpdateDto userUpdateDto)
    {
        try
        {
            return Ok(await _service.UpdateNameAndEmail(id, userUpdateDto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
