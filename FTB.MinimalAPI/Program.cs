using FTB.Common;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapPost("/api/CalculateElo/{winnerRating}/{loserRating}", (double winnerRating, double loserRating) =>
{
    Console.WriteLine($"Winner rating before Elo calculation: {winnerRating}");
    Console.WriteLine($"Loser rating before Elo calculation: {loserRating}");

    EloCalculationModel elo = EloCalculator.CalculateElo(winnerRating, loserRating);

    Console.WriteLine();
    Console.WriteLine($"New winner rating: {winnerRating + elo.WinnerRating}");
    Console.WriteLine($"New loser rating: {loserRating + elo.LoserRating}");

    return elo;
});

app.MapPost("/api/CalculateElo2/{winnerRating}/{loserRating}", (double winnerRating, double loserRating) => EloCalculator.CalculateElo(winnerRating, loserRating));

app.Run();
