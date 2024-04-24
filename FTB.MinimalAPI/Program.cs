using FTB.Common;
using FTB.MinimalAPI.Data;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("AutomobileDb") ?? throw new Exception("AutomobileDb Connection String does not exist!");
builder.Services.AddSqlite<AutomobileContext>(connectionString);

builder.Services.AddScoped<ThisAutomobileDoesNotExistService>();

builder.Services.AddCors();

WebApplication app = builder.Build();

await using AutomobileContext db = app.Services.CreateScope().ServiceProvider.GetRequiredService<AutomobileContext>();
db.Database.Migrate();

app.UseCors(appBuilder => appBuilder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.MapPost("/api/CalculateElo/{winnerRating:double}/{loserRating:double}", EloCalculator.CalculateElo);

app.MapGet("/api/GetCarPicture", (ThisAutomobileDoesNotExistService service) => service.GetRandomAutomobileImageAsync());

//app.MapPost("/api/GetAutomobile");

//app.MapPost("/api/GetVotes");

//app.MapGet("/api/GetResults");

//app.MapPut("/api/SubmitVote");

app.Run();
