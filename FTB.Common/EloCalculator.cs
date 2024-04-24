namespace FTB.Common
{
    // This class is used to calculate the Elo rating for a game.
    public static class EloCalculator
    {
        // KFactor is a constant used in the Elo rating system formula.
        private const int KFactor = 32;

        // This method calculates the new Elo ratings for the winner and loser of a game.
        public static EloCalculationModel CalculateElo(double winnerRating, double loserRating)
        {
            // Calculate the expected score for each player.
            // This is done using the formula: 1 / (1 + 10 ^ ((opponent's rating - player's rating) / 400))
            double winnerExpectedScore = 1 / (1 + Math.Pow(10, (loserRating - winnerRating) / 400));
            double loserExpectedScore = 1 / (1 + Math.Pow(10, (winnerRating - loserRating) / 400));

            // Update the ratings for each player.
            // The new rating is calculated by the formula: rating + KFactor * (actual score - expected score)
            // For the winner, the actual score is 1 and for the loser, it's 0.
            double winnerScore = KFactor * (1 - winnerExpectedScore);
            double loserScore = KFactor * (0 - loserExpectedScore);

            // Return the new ratings for the winner and loser.
            EloCalculationModel elo = new()
            {
                InitialWinnerRating = winnerRating,
                InitialLoserRating = loserRating,
                WinnerScore = winnerScore,
                LoserScore = loserScore,
                NewWinnerRating = winnerRating + winnerScore,
                NewLoserRating = loserRating + loserScore
            };
            return (elo);
        }
    }
}
