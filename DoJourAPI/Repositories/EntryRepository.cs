using System.Collections.Generic;
using System.Threading.Tasks;
using DoJourAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
