using FTB.Common;
using FTB.MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FTB.MinimalAPI.Data;

public interface IAutomobileRepository
{
    public Task<AutomobileModel> GetAutomobileAsync();
    public IEnumerable<AutomobileModel> GetResults();
    public IEnumerable<VoteModel> GetVotes();
}

public class AutomobileRepository(AutomobileContext context, ThisAutomobileDoesNotExistService automobileDoesNotExistService) : IAutomobileRepository
{
    public async Task<AutomobileModel> GetAutomobileAsync()
    {
        string automobileImage = await automobileDoesNotExistService.GetRandomAutomobileImageAsync();
        string automobileName;
        bool automobileNameExists = false;
        do
        {
            automobileName = automobileDoesNotExistService.GetRandomAutomobileName();
            automobileNameExists = context.Automobiles.AsNoTracking().Any(automobile => automobile.AutomobileName == automobileName);
        } while (automobileNameExists);

        AutomobileModel automobile = new()
        {
            AutomobileImage = automobileImage,
            AutomobileName = automobileName,
            Losses = 0,
            Score = 1200,
            Wins = 0
        };

        IEnumerable<AutomobileModel> automobiles = this.GetResults();
        foreach (AutomobileModel existingAutomobile in automobiles)
        {
            VoteModel vote = new()
            {
                Car1Name = automobileName,
                Car2Name = existingAutomobile.AutomobileName,
                Score = 0,
                Winner = null
            };
            context.Votes.Add(vote);
            await context.SaveChangesAsync();
        }

        context.Automobiles.Add(automobile);
        await context.SaveChangesAsync();
        return automobile;
    }

    public IEnumerable<AutomobileModel> GetResults()
    {
        IEnumerable<AutomobileModel> automobiles = context.Automobiles.AsNoTracking();
        return automobiles;
    }

    public IEnumerable<VoteModel> GetVotes()
    {
        IEnumerable<VoteModel> votes = context.Votes.Where(s => s.Winner == null).AsNoTracking();
        return votes;
    }

    public async Task SubmitVoteAsync(string id, string winner)
    {
        VoteModel existingVote = context.Votes.AsNoTracking().FirstOrDefault(vote => vote.Id == id) ?? throw new Exception("Vote does not exist!");
        if (existingVote.Winner != null)
        {
            return;
        }

        existingVote.Winner = winner;

        AutomobileModel car1 = context.Automobiles.AsNoTracking().FirstOrDefault(automobile => automobile.AutomobileName == existingVote.Car1Name) ?? throw new Exception("Car1 does not exist!");
        AutomobileModel car2 = context.Automobiles.AsNoTracking().FirstOrDefault(automobile => automobile.AutomobileName == existingVote.Car2Name) ?? throw new Exception("Car2 does not exist!");

        EloCalculationModel elo;
        if (winner == existingVote.Car1Name)
        {
            elo = EloCalculator.CalculateElo(car1.Score, car2.Score);
            car1.Wins++;
            car2.Losses++;
            car1.Score = elo.NewWinnerRating;
            car2.Score = elo.NewLoserRating;
        }
        else
        {
            elo = EloCalculator.CalculateElo(car2.Score, car1.Score);
            car2.Wins++;
            car1.Losses++;
            car2.Score = elo.NewWinnerRating;
            car1.Score = elo.NewLoserRating;
        }

        existingVote.Score = elo.WinnerScore;

        context.Automobiles.UpdateRange(car1, car2);
        await context.SaveChangesAsync();
        
        context.Votes.Update(existingVote);
        await context.SaveChangesAsync();
    }
}
