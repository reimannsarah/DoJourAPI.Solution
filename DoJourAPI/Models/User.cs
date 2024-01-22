using System.ComponentModel.DataAnnotations;

namespace DoJourAPI.Models 
{
  public class User
  {
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email  { get; set; }
    public string Username { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
    public ICollection<Entry> Entries { get; set; }
  }
}