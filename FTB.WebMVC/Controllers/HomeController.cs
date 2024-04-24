using FTB.WebMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FTB.WebMVC.Controllers;

[Authorize]
public class HomeController(ILogger<HomeController> logger) : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        logger.LogInformation("Inside Index");
        return this.View();
    }

    [AllowAnonymous]
    public IActionResult Privacy()
    {
        logger.LogInformation("Inside Privacy");
        return this.View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        logger.LogInformation("Inside Error");
        return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}
