using MongoDB.Driver;
using MongoDB.Driver.Linq;
using StudentCenterAuthApi.src.Domain.Interfaces;
using StudentCenterAuthApi.src.Domain.Model;
using StudentCenterAuthApi.src.Infrastructure.Data.Context;

namespace StudentCenterAuthApi.src.Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthContext _context;

    public UserRepository(AuthContext context)
    {
        _context = context;
    }

    public async Task<bool> DeleteByEmail(string email)
    {
        var result = await _context.Users.DeleteOneAsync(user => user.Email == email);
      
        return result.DeletedCount > 0;
    }
    public async Task<ICollection<User>> GetAllUsers()
    {
        var users = await _context.Users.Find(_ => true).ToListAsync();

        return users;
    }   
    public async Task<User> AddNewUser(User user)
    {
        await _context.Users.InsertOneAsync(user);

        return user;
    }
    public async Task<User> UpdateNameAndEmail(string id, User user)
    {
        await _context.Users.UpdateOneAsync(
                Builders<User>.Filter.Eq(u => u._id, id), 
                Builders<User>.Update
                .Set(u => u.Name, user.Name)   
                .Set(u => u.Email, user.Email) 
                );

        return user;
    }
    public async Task<User> UpdatePassword(string id, string passWord)
    {
        await _context.Users.UpdateOneAsync(
               Builders<User>.Filter.Eq(u => u._id, id),
               Builders<User>.Update
               .Set(u => u.PassWord, passWord)
               .Set(u => u.FirstAccess, true)
               .Set(u => u.LastAccess, DateTime.Now)
               );

        var user = await _context.Users.Find(u => u._id == id).FirstOrDefaultAsync();

        return user;
    }
    public async Task<User> UserLogin(User user)
    {
        var userLogin = await _context.Users
            .Find(u => u.Email == user.Email)
            .FirstOrDefaultAsync();

        if(userLogin == null) throw new Exception("User not found");

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(user.PassWord,userLogin.PassWord);

        if (!isPasswordValid) throw new Exception("Invalid Password");

        return userLogin;
    }
   
}
