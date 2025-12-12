using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public CompanyRole? CompanyRole { get; set; }
    
    public SiteRole SiteRole { get; set; }
    
    public LicenseType LicenseType { get; set; }

    // Foreign key
    public int CompanyId { get; set; }

    // Navigation property
    public Company Company { get; set; } = null!;
}

