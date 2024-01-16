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
}
