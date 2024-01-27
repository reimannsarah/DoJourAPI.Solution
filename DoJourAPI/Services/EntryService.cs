using DoJourAPI.Models;
using DoJourAPI.Repositories;

namespace DoJourAPI.Services;

public class EntryService : IEntryService
{
    private readonly IEntryRepository _entryRepository;

    public EntryService(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public async Task<IEnumerable<Entry>> GetAllEntriesAsync()
    {
        return await _entryRepository.GetAllAsync();
    }

    public async Task<Entry> GetEntryByIdAsync(Guid id)
    {
        return await _entryRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Entry>> GetEntriesByUserIdAsync(Guid userId)
    {
        var entries = await _entryRepository.GetByUserIdAsync(userId);
        return entries ?? new List<Entry>();
    }

    public async Task CreateEntryAsync(Entry entry)
    {
        await _entryRepository.CreateAsync(entry);
    }

    public async Task UpdateEntryAsync(Entry entry)
    {
        await _entryRepository.UpdateAsync(entry);
    }

    public async Task DeleteEntryAsync(Guid id)
    {
        await _entryRepository.DeleteAsync(id);
    }
}
