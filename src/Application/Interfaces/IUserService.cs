using StudentCenterAuthApi.src.Application.DTOs;

namespace StudentCenterAuthApi.src.Application.Interfaces;

/// <summary>
/// Defines operations related to user management in the system
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Retrieves all registered users
    /// </summary>
    /// <returns>A collection of user data transfer objects</returns>
    Task<ICollection<UserDto>> GetAllUsers();

    /// <summary>
    /// Adds a new user to the system
    /// </summary>
    /// <param name="user">The data required to create the new user</param>
    /// <returns>The created user’s data transfer object</returns>
    Task<UserDto> AddNewUser(UserCreateDto user);

    /// <summary>
    /// Deletes a user based on their email address
    /// </summary>
    /// <param name="email">The email address of the user to delete</param>
    /// <returns>True if the user was successfully deleted; otherwise, false</returns>
    Task<bool> DeleteByEmail(string email);

    /// <summary>
    /// Updates a user's name and email using their ID
    /// </summary>
    /// <param name="id">The unique identifier of the use</param>
    /// <param name="user">The updated name and email data</param>
    /// <returns>The updated user’s data transfer object</returns>
    Task<UserDto> UpdateNameAndEmail(string id, UserUpdateDto user);

    /// <summary>
    /// Updates the password of a user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="passWord">The new password to be set</param>
    /// <returns>A data object containing the user’s login information</returns>
    Task<UserDataLoginDto> UpdatePassword(string id, string passWord);

    /// <summary>
    /// Authenticates the user using their email and password
    /// </summary>
    /// <param name="Email">The user's email address</param>
    /// <param name="PassWord">The user's password</param>
    /// <returns>A data object containing the authenticated user's login information</returns>
    Task<UserDataLoginDto> AuthenticateUser(string Email, string PassWord);

    /// <summary>
    /// Updates the last access timestamp for a user
    /// </summary>
    /// <param name="Email">The user's email address</param>
    /// <param name="PassWord">The user's password</param>
    /// <returns>A data object containing updated login information</returns>
    Task<UserDataLoginDto> UpdateLastAccess(string Email, string PassWord);
}
