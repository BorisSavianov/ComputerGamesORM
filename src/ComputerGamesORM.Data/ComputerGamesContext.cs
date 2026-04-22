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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GameConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
