namespace FTB.BlazorWASM.Models;

public class VoteViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Car1Name { get; set; } = string.Empty;
    public string Car2Name { get; set; } = string.Empty;
    public string? Winner { get; set; }
    public double Score { get; set; } = 0;
}
