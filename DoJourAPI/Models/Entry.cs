namespace DoJourAPI.Models 
{
  public class Entry
  {
    public int EntryId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public string Title { get; set; }
    public string Subject { get; set; }
    public string Date { get; set; }
    public string Text { get; set; }
  }
}