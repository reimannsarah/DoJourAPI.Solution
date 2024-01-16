using System.Collections.Generic;
using System.Threading.Tasks;
using DoJourAPI.Models;

namespace DoJourAPI.Services;

public interface IEntryService
{
    // Task<Entry> GetEntryByIdAsync(int id);
    Task<IEnumerable<Entry>> GetAllEntriesAsync();
    // Task CreateEntryAsync(Entry entry);
    // Task UpdateEntryAsync(Entry entry);
    // Task DeleteEntryAsync(int id);
}
