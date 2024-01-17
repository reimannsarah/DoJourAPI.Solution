using DoJourAPI.Models;

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
