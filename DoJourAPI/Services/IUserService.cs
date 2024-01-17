using DoJourAPI.Models;

namespace DoJourAPI.Services;

public interface IUserService
{
    Task<User> GetUserByIdAsync(Guid id);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid id);
}