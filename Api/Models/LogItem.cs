namespace Api.Models;

public class LogItem
{
    public Guid Id { get; set; }
    public string Discrepancy { get; set; } = string.Empty;
    public bool SignedOff { get; set; } = false;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime? SignedOffOn { get; set; } = null;
}