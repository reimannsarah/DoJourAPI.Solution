using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoJourAPI.Repositories;
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
    public async Task<IActionResult> GetEntryById(int id)
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
    public async Task<IActionResult> UpdateEntry(int id, Entry entry)
    {
        if (id != entry.EntryId)
        {
            return BadRequest();
        }
        await _entryService.UpdateEntryAsync(entry);
        return NoContent();
    }
}
