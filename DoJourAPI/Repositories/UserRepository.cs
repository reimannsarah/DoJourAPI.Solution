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

}