using DoJourAPI.Models;
using DoJourAPI.Repositories;

namespace DoJourAPI.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;

  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<User> GetUserByIdAsync(Guid id)
  {
    return await _userRepository.GetByIdAsync(id);
  }

  public async Task<User> GetUserByEmailAsync(string email)
  {
    try
    {
      var user = await _userRepository.GetByEmailAsync(email);
      return user;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
      throw;
    }
  }

  public async Task CreateUserAsync(User user)
{
    try
    {
        await _userRepository.CreateAsync(user);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        throw;
    }
}

  public async Task UpdateUserAsync(User user)
  {
    await _userRepository.UpdateAsync(user);
  }

  public async Task DeleteUserAsync(Guid id)
  {
    await _userRepository.DeleteAsync(id);
  }
}