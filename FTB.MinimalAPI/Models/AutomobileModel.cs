using System.ComponentModel.DataAnnotations;

namespace FTB.MinimalAPI.Models;

public class AutomobileModel
{
    [Key]
    [MaxLength(100)]
    public string AutomobileName { get; set; } = string.Empty;

    [MaxLength(300)]
    public string AutomobileImage { get; set; } = string.Empty;

    public double Score { get; set; } = 1200;

    public int Wins { get; set; } = 0;

    public int Losses { get; set; } = 0;
}
