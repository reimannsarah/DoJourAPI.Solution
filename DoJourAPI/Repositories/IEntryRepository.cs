using System.Collections.Generic;
using System.Threading.Tasks;
using DoJourAPI.Models;
using DoJourAPI.Repositories;

namespace DoJourAPI.Repositories
{
    public interface IEntryRepository
    {
        Task<Entry> GetByIdAsync(int id);
        Task<IEnumerable<Entry>> GetAllAsync();
        Task CreateAsync(Entry entry);
        Task UpdateAsync(Entry entry);
        Task DeleteAsync(int id);
    }
}
