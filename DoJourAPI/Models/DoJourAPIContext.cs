using Microsoft.EntityFrameworkCore;

namespace DoJourAPI.Models
{
  public class DoJourAPIContext : DbContext
  {
    public DoJourAPIContext() {}
    public virtual DbSet<Entry> Entries { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public DoJourAPIContext(DbContextOptions<DoJourAPIContext> options) : base(options) {}
  }
}