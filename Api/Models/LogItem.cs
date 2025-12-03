using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

public class LogItem
{
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Discrepancy { get; set; } = string.Empty;
    public bool SignedOff { get; set; } = false;
    public DateTime CreatedOn { get; set; }
    public DateTime? SignedOffOn { get; set; } = null;
}