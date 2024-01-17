using Microsoft.EntityFrameworkCore;

namespace DoJourAPI.Models
{
  public class DoJourAPIContext : DbContext
  {
    public DoJourAPIContext() {}
    public virtual DbSet<Entry> Entries { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public DoJourAPIContext(DbContextOptions<DoJourAPIContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entry>()
            .HasOne(e => e.User)
            .WithMany(u => u.Entries)
            .HasForeignKey(e => e.UserId);
    }
  }
}