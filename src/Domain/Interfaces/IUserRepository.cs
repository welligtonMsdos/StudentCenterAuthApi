using StudentCenterAuthApi.src.Domain.Model;

namespace StudentCenterAuthApi.src.Domain.Interfaces;

public interface IUserRepository 
{
    Task<ICollection<User>> GetAllUsers();   
    Task<User> AddNewUser(User user);
    Task<bool> DeleteByEmail(string email);
    Task<User> UpdateNameAndEmail(string id, User user);
    Task<User> UpdatePassword(string id, string passWord);
    Task<User> AuthenticateUser(User user);
    Task<User> UpdateLastAccess(User user);
}