using DoJourAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DoJourAPI.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly DoJourAPIContext _context;

        public EntryRepository(DoJourAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entry>> GetAllAsync()
        {
            return await _context.Entries.ToListAsync();
        }

        public async Task<Entry> GetByIdAsync(Guid id)
        {
            return await _context.Entries.SingleOrDefaultAsync(entry => entry.EntryId == id);
        }

        public async Task<IEnumerable<Entry>> GetByUserIdAsync(Guid userId)
        {
            var userExists = await _context.Users.AnyAsync(user => user.UserId == userId);
            if (!userExists)
            {
                throw new Exception("User not found");
            }

            return await _context.Entries
                .Where(entry => entry.UserId == userId)
                .ToListAsync();
        }

        public async Task CreateAsync(Entry entry)
        {
            await _context.Entries.AddAsync(entry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Entry entry)
        {
            _context.Entries.Update(entry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entry = await GetByIdAsync(id);
            _context.Entries.Remove(entry);
            await _context.SaveChangesAsync();
        }
    }
}
