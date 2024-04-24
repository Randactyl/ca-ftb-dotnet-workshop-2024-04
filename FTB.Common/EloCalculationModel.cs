namespace FTB.Common;

// This class represents the model for Elo rating calculation.
public class EloCalculationModel
{
    // Initial rating of the winner before the game.
    public double InitialWinnerRating { get; set; }

    // Initial rating of the loser before the game.
    public double InitialLoserRating { get; set; }

    // Current rating of the winner.
    public double WinnerRating { get; set; }

    // Current rating of the loser.
    public double LoserRating { get; set; }

    // New calculated rating of the winner after the game.
    public double NewWinnerRating { get; set; }

    // New calculated rating of the loser after the game.
    public double NewLoserRating { get; set; }
}
