using System.ComponentModel.DataAnnotations;

namespace FTB.MinimalAPI.Models;

public class VoteModel
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Car1Name { get; set; } = string.Empty;
    public string Car2Name { get; set; } = string.Empty;
    public string? Winner { get; set; }
    public double Score { get; set; } = 0;
}
