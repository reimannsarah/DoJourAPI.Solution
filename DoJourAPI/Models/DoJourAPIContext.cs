using Microsoft.EntityFrameworkCore;

namespace DoJourAPI.Models
{
  public class DoJourAPIContext : DbContext
  {
    public DbSet<Entry> Entries { get; set; }
    public DoJourAPIContext(DbContextOptions<DoJourAPIContext> options) : base(options) {}
  }
}