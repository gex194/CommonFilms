using CommonFilms.Models.Entities;

namespace CommonFilms.Services.UserService;

public interface IUserService
{
    public Task<User?> GetByIdAsync(int id);
    public Task<User?> GetByEmailAsync(string email);
    public Task<User> CreateAsync(User user);
    public Task<User> UpdateAsync(User user);
    public Task<bool> DeleteAsync(int id);
}