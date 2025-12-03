using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dtos;

public class CreateLogItemDto
{
    [Required(ErrorMessage = "Discrepancy description is required")]
    public string Discrepancy { get; set; } = string.Empty;
}