using FTB.Common;
using Microsoft.AspNetCore.Mvc;

namespace FTB.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EloController(ILogger<EloController> logger) : ControllerBase
{
    [HttpPost("{winnerRating}/{loserRating}")]
    public IActionResult Get(double winnerRating, double loserRating)
    {
        logger.LogInformation("Entered the Elo endpoint");

        logger.LogInformation("--------------------");
        logger.LogInformation("Winner rating before Elo calculation: {0}", winnerRating);
        logger.LogInformation("Loser rating before Elo calculation: {0}", loserRating);

        EloCalculationModel elo = EloCalculator.CalculateElo(winnerRating, loserRating);

        logger.LogInformation("--------------------");
        logger.LogInformation("New winner rating: {0}", winnerRating + elo.NewWinnerRating);
        logger.LogInformation("New loser rating: {0}", loserRating + elo.NewLoserRating);
        logger.LogInformation("--------------------");

        return this.Ok(elo);
    }
}
