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

  public async Task<IEnumerable<User>> GetAllAsync()
  {
    return await _context.Users.ToListAsync();
  }

  public async Task<User> GetByIdAsync(Guid id)
  {
    return await _context.Users.SingleOrDefaultAsync(user => user.UserId == id);
  }

  public async Task<User> CreateAsync(User user)
  {
    await _context.Users.AddAsync(user);
    await _context.SaveChangesAsync();
    return user;
  }

  public async Task UpdateAsync(User user)
  {
    _context.Users.Update(user);
    await _context.SaveChangesAsync();
  }
}