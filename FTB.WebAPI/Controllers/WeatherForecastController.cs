using Microsoft.AspNetCore.Mvc;

namespace FTB.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    private static readonly string[] Summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        logger.LogInformation("Entered the GetWeatherForecast endpoint");

        WeatherForecast[] weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

        logger.LogInformation($"Got {weatherForecasts.Length} forecasts");
        foreach (WeatherForecast weatherForecast in weatherForecasts)
        {
            logger.LogInformation("---------------------");
            logger.LogInformation($"The date is: {weatherForecast.Date}");
            logger.LogInformation($"The forecast is {weatherForecast.Summary}");
            logger.LogInformation($"The C is: {weatherForecast.TemperatureC}");
            logger.LogInformation($"The F is: {weatherForecast.TemperatureF}");
        }

        return weatherForecasts;
    }
}
