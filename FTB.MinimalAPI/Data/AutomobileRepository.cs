using FTB.Common;
using FTB.MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FTB.MinimalAPI.Data;

public interface IAutomobileRepository
{
    public Task<AutomobileModel> GetAutomobileAsync();
    public IEnumerable<AutomobileModel> GetResults();
    public IEnumerable<VoteViewModel> GetVotes();
    public IEnumerable<VoteViewModel> GetAllVotes();
    public Task SubmitVoteAsync(string id, string winner);
    public Task ResetVotes();
    public void DeleteAutomobile(string automobileName);
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

    public IEnumerable<VoteViewModel> GetVotes()
    {
        List<VoteViewModel> voteVMs = [];
        IEnumerable<VoteModel> votes = context.Votes.AsNoTracking().Where(s => s.Winner == null);
        foreach (VoteModel vote in votes)
        {
            AutomobileModel car1 = context.Automobiles.AsNoTracking().FirstOrDefault(automobile => automobile.AutomobileName == vote.Car1Name) ?? throw new Exception("Car1 does not exist!");
            AutomobileModel car2 = context.Automobiles.AsNoTracking().FirstOrDefault(automobile => automobile.AutomobileName == vote.Car2Name) ?? throw new Exception("Car2 does not exist!");

            voteVMs.Add(new VoteViewModel
            {
                Id = vote.Id,
                Car1Name = vote.Car1Name,
                Car1Image = car1.AutomobileImage,
                Car2Name = vote.Car2Name,
                Car2Image = car2.AutomobileImage,
                Score = vote.Score
            });
        }

        return voteVMs;
    }

    public IEnumerable<VoteViewModel> GetAllVotes()
    {
        IEnumerable<VoteModel> votes = context.Votes.AsNoTracking();
        IEnumerable<VoteViewModel> voteVMs = votes.Select(vote => new VoteViewModel
        {
            Id = vote.Id,
            Car1Name = vote.Car1Name,
            Car2Name = vote.Car2Name,
            Score = vote.Score,
            Winner = vote.Winner
        });

        return voteVMs;
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

    public async Task ResetVotes()
    {
        IEnumerable<VoteModel> votes = context.Votes.AsNoTracking().Where(vote => vote.Winner != null);
        foreach (VoteModel vote in votes)
        {
            vote.Winner = null;
            vote.Score = 0;
            context.Votes.Update(vote);
            await context.SaveChangesAsync();
        }

        IEnumerable<AutomobileModel> automobiles = context.Automobiles.AsNoTracking();
        foreach (AutomobileModel automobile in automobiles)
        {
            automobile.Wins = 0;
            automobile.Losses = 0;
            automobile.Score = 1200;
            context.Automobiles.Update(automobile);
            await context.SaveChangesAsync();
        }
    }

    public void DeleteAutomobile(string automobileName)
    {
        List<VoteModel> votes = context.Votes.Where(s => s.Car1Name == automobileName || s.Car2Name == automobileName).ToList();
        foreach (VoteModel vote in votes)
        {
            context.Votes.Remove(vote);
            context.SaveChanges();

            if (string.IsNullOrEmpty(vote.Winner))
            {
                continue;
            }

            string competitor = vote.Car1Name == automobileName ? vote.Car2Name : vote.Car1Name;
            AutomobileModel? competingCar = context.Automobiles.AsNoTracking().FirstOrDefault(s => s.AutomobileName == competitor);
            if (competingCar is null)
            {
                continue;
            }

            if (vote.Winner == competitor)
            {
                competingCar.Score -= vote.Score;
                competingCar.Wins--;
            }
            else
            {
                competingCar.Score += vote.Score;
                competingCar.Losses--;
            }

            context.Automobiles.Update(competingCar);
            context.SaveChanges();
        }

        AutomobileModel? carByName = context.Automobiles.AsNoTracking().FirstOrDefault(s => s.AutomobileName == automobileName);
        if (carByName is null)
        {
            return;
        }

        context.Automobiles.Remove(carByName);
        context.SaveChanges();
    }
}
