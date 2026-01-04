using System.ComponentModel.DataAnnotations;
using Api.Models.Dtos.Teamss;

namespace Api.Models.Dtos.Users;

public class CreateUserProfileDto
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(70)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public LicenseType LicenseType { get; set; }
}

public class BasicUserProfileReturnDto
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public LicenseType LicenseType { get; set; }
    
    public BasicTeamReturnDto? Team { get; set; }
}

public class UpdateUserProfileDto
{
    [MaxLength(100)]
    public string? FirstName { get; set; }
    
    [MaxLength(70)]
    public string? LastName { get; set; }
    
    public LicenseType? LicenseType { get; set; }
}