using FTB.Common;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ThisAutomobileDoesNotExistService>();

WebApplication app = builder.Build();

app.MapPost("/api/CalculateElo/{winnerRating:double}/{loserRating:double}", EloCalculator.CalculateElo);

app.MapGet("/api/GetCarPicture", (ThisAutomobileDoesNotExistService service) => service.GetRandomAutomobileImageAsync());

app.Run();
