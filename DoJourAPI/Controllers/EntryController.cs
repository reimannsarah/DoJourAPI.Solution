using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoJourAPI.Repositories;
using DoJourAPI.Services;

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
}
