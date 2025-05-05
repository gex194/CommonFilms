using CommonFilms.Models.Entities;
using CommonFilms.Repositories.UserRepository;

namespace CommonFilms.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        return user;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users;
    }

    public async Task<User> UpdateAsync(User user)
    {
        return await _userRepository.UpdateAsync(user);
    }

    public async Task<User> CreateAsync(User user)
    {
        return await _userRepository.CreateAsync(user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }
}