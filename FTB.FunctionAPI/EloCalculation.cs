using FTB.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FTB.FunctionAPI;

public class EloCalculation(ILogger<EloCalculation> logger)
{
    [Function(nameof(EloCalculation))]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "EloCalculation/{winnerRating}/{loserRating}")] HttpRequest req, 
        string winnerRating, string loserRating)
    {
        // Calculate the new Elo ratings for the winner and loser.
        EloCalculationModel elo = EloCalculator.CalculateElo(Convert.ToDouble(winnerRating), Convert.ToDouble(loserRating));

        logger.LogInformation("--------------------");
        logger.LogInformation("Winner rating before Elo calculation: {0}", winnerRating);
        logger.LogInformation("Loser rating before Elo calculation: {0}", loserRating);
        logger.LogInformation("Winner score: {0}", elo.WinnerScore);
        logger.LogInformation("Loser score: {0}", elo.LoserScore);
        logger.LogInformation("New winner rating: {0}", elo.NewWinnerRating);
        logger.LogInformation("New loser rating: {0}", elo.NewLoserRating);
        logger.LogInformation("--------------------");

        // return the new Elo ratings as the response.
        return new OkObjectResult(elo);
    }
}
