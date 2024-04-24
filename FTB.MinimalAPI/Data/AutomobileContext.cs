using FTB.MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FTB.MinimalAPI.Data;

public class AutomobileContext : DbContext
{
    public AutomobileContext(DbContextOptions options) : base(options) { }

    public DbSet<AutomobileModel> Automobiles { get; set; }
    public DbSet<VoteModel> Votes { get; set; }
}
