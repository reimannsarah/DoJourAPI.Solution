using Microsoft.AspNetCore.Mvc;
using DoJourAPI.Services;
using DoJourAPI.Models;

namespace DoJourAPI.Controllers;

[ApiController]
[Route("api/entries")]
public class EntriesController : ControllerBase
{
    private readonly IEntryService _entryService;

    public EntriesController(IEntryService entryService)
    {
        _entryService = entryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEntries()
    {
        var entries = await _entryService.GetAllEntriesAsync();
        return Ok(entries);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEntryById(Guid id)
    {
        var entry = await _entryService.GetEntryByIdAsync(id);
        if (entry == null)
        {
            return NotFound();
        }
        return Ok(entry);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEntry(Entry entry)
    {
        await _entryService.CreateEntryAsync(entry);
        return CreatedAtAction(nameof(GetEntryById), new { id = entry.EntryId }, entry);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEntry(Guid id, Entry entry)
    {
        if (id != entry.EntryId)
        {
            return BadRequest();
        }
        await _entryService.UpdateEntryAsync(entry);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEntry(Guid id)
    {
        await _entryService.DeleteEntryAsync(id);
        return NoContent();
    }
}
