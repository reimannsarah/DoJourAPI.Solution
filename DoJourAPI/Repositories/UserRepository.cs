using DoJourAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DoJourAPI.Repositories;

public class UserRepository : IUserRepository
{
  private readonly DoJourAPIContext _context;
  public UserRepository(DoJourAPIContext context)
  {
    _context = context;
  }
  public async Task<User> GetByIdAsync(Guid id)
  {
    return await _context.Users.SingleOrDefaultAsync(user => user.UserId == id);
  }

  public async Task<User> GetByEmailAsync(string email)
  {
    return await _context.Users.SingleOrDefaultAsync(user => user.Email == email);
  }

  public async Task CreateAsync(User user)
  {
    await _context.Users.AddAsync(user);
    await _context.SaveChangesAsync();
  }

  public async Task UpdateAsync(User user)
  {
    _context.Users.Update(user);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteAsync(Guid id)
  {
    var user = await GetByIdAsync(id);
    _context.Users.Remove(user);
    await _context.SaveChangesAsync();
  }
}