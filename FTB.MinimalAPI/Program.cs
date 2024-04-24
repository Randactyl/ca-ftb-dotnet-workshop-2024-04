using FTB.Common;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ThisAutomobileDoesNotExistService>();

builder.Services.AddCors();

WebApplication app = builder.Build();

app.UseCors(appBuilder => appBuilder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.MapPost("/api/CalculateElo/{winnerRating:double}/{loserRating:double}", EloCalculator.CalculateElo);

app.MapGet("/api/GetCarPicture", (ThisAutomobileDoesNotExistService service) => service.GetRandomAutomobileImageAsync());

app.Run();
