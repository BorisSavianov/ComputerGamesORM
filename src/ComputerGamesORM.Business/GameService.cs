using ComputerGamesORM.Data;
using ComputerGamesORM.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerGamesORM.Business;

public sealed class GameService : IGameService
{
    private readonly ComputerGamesContext _context;

    public GameService(ComputerGamesContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<GameDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Games
            .AsNoTracking()
            .OrderBy(g => g.Id)
            .Select(g => new GameDto(g.Id, g.Name))
            .ToListAsync(cancellationToken);
    }

    public async Task<GameDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            return null;
        }

        return await _context.Games
            .AsNoTracking()
            .Where(g => g.Id == id)
            .Select(g => new GameDto(g.Id, g.Name))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> AddAsync(string gameName, CancellationToken cancellationToken = default)
    {
        var normalizedName = NormalizeName(gameName);

        var game = new Game
        {
            Name = normalizedName
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync(cancellationToken);
        return game.Id;
    }

    public async Task<bool> UpdateAsync(int id, string gameName, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            return false;
        }

        var normalizedName = NormalizeName(gameName);
        var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

        if (game is null)
        {
            return false;
        }

        game.Name = normalizedName;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            return false;
        }

        var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

        if (game is null)
        {
            return false;
        }

        _context.Games.Remove(game);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static string NormalizeName(string gameName)
    {
        if (string.IsNullOrWhiteSpace(gameName))
        {
            throw new ArgumentException("Game name cannot be empty.", nameof(gameName));
        }

        var normalizedName = gameName.Trim();
        if (normalizedName.Length > 200)
        {
            throw new ArgumentException("Game name must be up to 200 characters.", nameof(gameName));
        }

        return normalizedName;
    }
}
