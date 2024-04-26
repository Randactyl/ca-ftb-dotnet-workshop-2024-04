using FTB.TriviaLib;

namespace FTB.TriviaWeb.Models;

public class InProgressGame
{
    public GameSettingsModel Settings { get; set; } = new();
    public OpenTriviaModel OpenTriviaModel { get; set; } = new();
}
