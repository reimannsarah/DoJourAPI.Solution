using System.Collections.Generic;
using System.Threading.Tasks;
using DoJourAPI.Models;
using DoJourAPI.Repositories;
using DoJourAPI.Services;

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

    public async Task<Entry> GetEntryByIdAsync(int id)
    {
        return await _entryRepository.GetByIdAsync(id);
    }

    public async Task CreateEntryAsync(Entry entry)
    {
        await _entryRepository.CreateAsync(entry);
    }

    public async Task UpdateEntryAsync(Entry entry)
    {
        await _entryRepository.UpdateAsync(entry);
    }

    public async Task DeleteEntryAsync(int id)
    {
        await _entryRepository.DeleteAsync(id);
    }
}
