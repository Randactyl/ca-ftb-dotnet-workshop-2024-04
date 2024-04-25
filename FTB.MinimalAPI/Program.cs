using FTB.Common;
using FTB.MinimalAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("AutomobileDb") ?? throw new Exception("AutomobileDb Connection String does not exist!");
builder.Services.AddSqlite<AutomobileContext>(connectionString);

builder.Services.AddScoped<AutomobileRepository>();
builder.Services.AddScoped<IAutomobileRepository>(sp => sp.GetRequiredService<AutomobileRepository>());
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

app.MapPost("/api/GetNewAutomobile", ([FromServices] IAutomobileRepository repo) => repo.GetAutomobileAsync() );

app.MapGet("/api/GetResults", ([FromServices] IAutomobileRepository repo) => repo.GetResults());

app.MapPost("/api/GetVotes", ([FromServices] IAutomobileRepository repo) => repo.GetVotes());

app.MapGet("/api/GetAllVotes", ([FromServices] IAutomobileRepository repo) => repo.GetAllVotes());

app.MapPut("/api/SubmitVote/{voteId}/{winnerCarName}", (string voteId, string winnerCarName, [FromServices] IAutomobileRepository repo) => repo.SubmitVoteAsync(voteId, winnerCarName));

app.MapDelete("/api/ResetVotes", ([FromServices] IAutomobileRepository repo) => repo.ResetVotes());

app.MapDelete("/api/DeleteAutomobile/{automobileName}", (string automobileName, [FromServices] IAutomobileRepository repo) => repo.DeleteAutomobile(automobileName));

app.Run();
