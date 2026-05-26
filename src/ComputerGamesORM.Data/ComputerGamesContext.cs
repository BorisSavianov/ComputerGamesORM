using ComputerGamesORM.Data.Configurations;
using ComputerGamesORM.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerGamesORM.Data;

public class ComputerGamesContext : DbContext
{
    public ComputerGamesContext(DbContextOptions<ComputerGamesContext> options)
        : base(options)
    {
    }

    public DbSet<Game> Games => Set<Game>();

    public DbSet<GameDescription> GameDescriptions => Set<GameDescription>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GameConfiguration());
        modelBuilder.ApplyConfiguration(new GameDescriptionConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
