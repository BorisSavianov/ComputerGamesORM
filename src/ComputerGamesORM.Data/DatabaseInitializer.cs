using ComputerGamesORM.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerGamesORM.Data;

public sealed class DatabaseInitializer
{
    private readonly IDbContextFactory<ComputerGamesContext> _contextFactory;

    public DatabaseInitializer(IDbContextFactory<ComputerGamesContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await InitializeAsync(context, cancellationToken);
    }

    private static async Task InitializeAsync(ComputerGamesContext context, CancellationToken cancellationToken)
    {
        await context.Database.MigrateAsync(cancellationToken);

        if (await context.Games.AnyAsync(cancellationToken))
        {
            return;
        }

        context.Games.AddRange(
            new Game
            {
                Name = "Half-Life 2",
                GameDescription = new GameDescription
                {
                    Description = "A story-driven first-person shooter focused on physics, exploration, and resistance against the Combine."
                }
            },
            new Game
            {
                Name = "Portal 2",
                GameDescription = new GameDescription
                {
                    Description = "A puzzle game built around portal mechanics, test chambers, and sharp environmental storytelling."
                }
            },
            new Game
            {
                Name = "Hades",
                GameDescription = new GameDescription
                {
                    Description = "A fast roguelike action game where repeated escape attempts gradually reveal character stories."
                }
            });

        await context.SaveChangesAsync(cancellationToken);
    }
}
