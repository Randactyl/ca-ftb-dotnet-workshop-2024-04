namespace FTB.TriviaWeb.Models;

/// <summary>
/// This combination of settings doesnt have questions.
/// </summary>
public record GameSettingsModel
{
    public string Category { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public string GameType { get; set; } = string.Empty;
}
