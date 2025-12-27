using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Team
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string TeamName { get; set; } = string.Empty;

    // Navigation property - collection of users
    public ICollection<UserProfile> Users { get; set; } = new List<UserProfile>();
}