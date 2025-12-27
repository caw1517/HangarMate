using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Vehicle
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(30)] 
    public string Make { get; set; } = string.Empty;
    
    [MaxLength(30)]
    public string Model { get; set; } = string.Empty;

    [MaxLength(30)] 
    public string? SubModel { get; set; }
    
    [MaxLength(25)]
    public string? Vin { get; set; }
    
    [MaxLength(70)]
    public string? SerialNumber { get; set; }
    
    public Guid? UserOwnerId { get; set; }
    
    public UserProfile? UserOwner { get; set; }
    
    public int? TeamOwnerId { get; set; }
    
    public Team? TeamOwner { get; set; }
    
    public DateTime? CreatedOn { get; set; }
    
    [MaxLength(300)]
    public string? Notes { get; set; }
    
}