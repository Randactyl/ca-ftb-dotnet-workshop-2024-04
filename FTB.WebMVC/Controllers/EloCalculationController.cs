using FTB.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTB.WebMVC.Controllers;

[Authorize]
public class EloCalculationController(ILogger<EloCalculationController> logger) : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        logger.LogInformation("Inside Index");
        return this.View();
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Calculate(double winnerRating, double loserRating)
    {
        logger.LogInformation("Inside Calculate");

        EloCalculationModel elo = EloCalculator.CalculateElo(winnerRating, loserRating);

        return this.View("Index", elo);
    }
}
