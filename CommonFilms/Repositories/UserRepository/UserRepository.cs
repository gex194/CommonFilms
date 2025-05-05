using CommonFilms.Data;
using CommonFilms.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonFilms.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
    
    public async Task<User> CreateAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
    
    public async Task<User> UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
         await _dbContext.SaveChangesAsync();
         return user;
    }
    
    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
        {
            return false;
        }
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}