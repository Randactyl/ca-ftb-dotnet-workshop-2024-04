namespace FTB.TriviaWeb.Models;

public class ScoreViewModel
{
    public int NumberOfQuestions { get; set; }
    public int NumberOfCorrectAnswers { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public string GameType { get; set; } = string.Empty;
}
