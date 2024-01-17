namespace DoJourAPI.Models 
{
  public class Entry
  {
    public Guid EntryId { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string Title { get; set; }
    public string Subject { get; set; }
    public string Date { get; set; }
    public string Text { get; set; }
  }
}