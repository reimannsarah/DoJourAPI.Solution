using DoJourAPI.Models;

namespace DoJourAPI.Services;

public interface IEntryService
{
    Task<Entry> GetEntryByIdAsync(Guid id);
    Task<IEnumerable<Entry>> GetAllEntriesAsync();
    Task<IEnumerable<Entry>> GetEntriesByUserIdAsync(Guid userId);
    Task CreateEntryAsync(Entry entry);
    Task UpdateEntryAsync(Entry entry);
    Task DeleteEntryAsync(Guid id);
}
