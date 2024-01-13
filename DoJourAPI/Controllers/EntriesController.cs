using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoJourAPI.Models;

namespace DoJourAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EntriesController : ControllerBase
{
  private bool EntryExists(int id)
  {
    return _db.Entries.Any(entry => entry.EntryId == id);
  }

  private readonly DoJourAPIContext _db;

  public EntriesController(DoJourAPIContext db)
  {
    _db = db;
  }
    [HttpGet]
#nullable enable
  public async Task<ActionResult<IEnumerable<Entry>>> Get(string? title, string? date, string? subject)
  {
    var query = _db.Entries.AsQueryable();

    if (title != null)
    {
      query = query.Where(entry => entry.Title == title);
    }

    if (date != null)
    {
      query = query.Where(entry => entry.Date == date);
    }

    return await query.ToListAsync();
  }
#nullable disable
}