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

    public Task<bool> Delete(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<User>> GetAll()
    {
        var users = await _context.Users.Find(_ => true).ToListAsync();

        return users;
    }

    public Task<User> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> Post(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> Put(User entity)
    {
        throw new NotImplementedException();
    }
}
