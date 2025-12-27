using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Models.Enums;

namespace Api.Models;

public class UserProfile
{
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public TeamRole? TeamRole { get; set; }
    
    public SiteRole SiteRole { get; set; }
    
    public LicenseType LicenseType { get; set; }

    [ForeignKey(nameof(Team))]
    public int? TeamId { get; set; }
    public Team? Team { get; set; } = null!;
}

