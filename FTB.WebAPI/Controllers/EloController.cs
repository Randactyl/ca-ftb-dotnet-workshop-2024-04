using FTB.Common;
using Microsoft.AspNetCore.Mvc;

namespace FTB.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EloController(ILogger<EloController> logger) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        logger.LogInformation("Entered the Elo endpoint");

        double winnerRating = 1200;
        double loserRating = 1000;

        logger.LogInformation("--------------------");
        logger.LogInformation("Winner rating before Elo calculation: {0}", winnerRating);
        logger.LogInformation("Loser rating before Elo calculation: {0}", loserRating);

        EloCalculationModel elo = EloCalculator.CalculateElo(winnerRating, loserRating);

        logger.LogInformation("--------------------");
        logger.LogInformation("New winner rating: {0}", winnerRating + elo.WinnerRating);
        logger.LogInformation("New loser rating: {0}", loserRating + elo.LoserRating);
        logger.LogInformation("--------------------");

        return this.Ok(elo);
    }
}
