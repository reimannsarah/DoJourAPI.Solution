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
    }
}
