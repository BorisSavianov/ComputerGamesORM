using ComputerGamesORM.Data.Models;
using ComputerGamesORM.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace ComputerGamesORM.Data.Repositories;

public sealed class GameRepository : IGameRepository
{
    private readonly IDbContextFactory<ComputerGamesContext> _contextFactory;

    public GameRepository(IDbContextFactory<ComputerGamesContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IReadOnlyCollection<Game>> GetAllAsync(string? searchText = null, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var query = context.Games
            .AsNoTracking()
            .Include(g => g.GameDescription)
            .AsQueryable();

        var normalizedSearch = searchText?.Trim();
        if (!string.IsNullOrWhiteSpace(normalizedSearch))
        {
            if (int.TryParse(normalizedSearch, out var id) && id > 0)
            {
                query = query.Where(g => g.Id == id);
            }
            else
            {
                var searchPattern = $"%{EscapeLikePattern(normalizedSearch)}%";
                query = query.Where(g =>
                    EF.Functions.Like(g.Name, searchPattern, "\\") ||
                    (g.GameDescription != null && EF.Functions.Like(g.GameDescription.Description, searchPattern, "\\")));
            }
        }

        return await query
            .OrderBy(g => g.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Game?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            return null;
        }

        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var query = context.Games
            .AsNoTracking()
            .Include(g => g.GameDescription)
            .AsQueryable();

        return await query.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludedGameId = null, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var normalizedName = name.Trim();

        return await context.Games
            .AsNoTracking()
            .AnyAsync(g =>
                g.Name == normalizedName &&
                (!excludedGameId.HasValue || g.Id != excludedGameId.Value),
                cancellationToken);
    }

    public async Task<int> AddAsync(Game game, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await context.Games.AddAsync(game, cancellationToken);
        await SaveChangesWithConflictHandlingAsync(context, cancellationToken);
        return game.Id;
    }

    public async Task<bool> UpdateAsync(int id, string name, string description, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            return false;
        }

        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var game = await context.Games
            .Include(g => g.GameDescription)
            .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

        if (game is null)
        {
            return false;
        }

        game.Name = name;
        if (game.GameDescription is null)
        {
            game.GameDescription = new GameDescription
            {
                GameId = game.Id,
                Description = description
            };
        }
        else
        {
            game.GameDescription.Description = description;
        }

        await SaveChangesWithConflictHandlingAsync(context, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            return false;
        }

        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var game = await context.Games
            .Include(g => g.GameDescription)
            .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

        if (game is null)
        {
            return false;
        }

        context.Games.Remove(game);
        await SaveChangesWithConflictHandlingAsync(context, cancellationToken);
        return true;
    }

    private static async Task SaveChangesWithConflictHandlingAsync(ComputerGamesContext context, CancellationToken cancellationToken)
    {
        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
        {
            throw new DataConflictException("A database uniqueness constraint was violated.", ex);
        }
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException exception)
    {
        if (exception.InnerException is SqlException sqlException &&
            sqlException.Number is 2601 or 2627)
        {
            return true;
        }

        var message = exception.InnerException?.Message ?? exception.Message;
        return message.Contains("IX_Games_Name", StringComparison.OrdinalIgnoreCase) ||
               message.Contains("UNIQUE constraint failed", StringComparison.OrdinalIgnoreCase) ||
               message.Contains("duplicate", StringComparison.OrdinalIgnoreCase);
    }

    private static string EscapeLikePattern(string value)
    {
        return value
            .Replace("\\", "\\\\", StringComparison.Ordinal)
            .Replace("%", "\\%", StringComparison.Ordinal)
            .Replace("_", "\\_", StringComparison.Ordinal);
    }
}
