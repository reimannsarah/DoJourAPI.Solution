using DoJourAPI.Models;

namespace DoJourAPI.Repositories
{
    public interface IEntryRepository
    {
        Task<Entry> GetByIdAsync(Guid id);
        Task<IEnumerable<Entry>> GetAllAsync();
        Task<IEnumerable<Entry>> GetByUserIdAsync(Guid userId);
        Task CreateAsync(Entry entry);
        Task UpdateAsync(Entry entry);
        Task DeleteAsync(Guid id);
    }
}
