using ComputerGamesORM.Data.Models;
using ComputerGamesORM.Data.Repositories;
using ComputerGamesORM.Data.Exceptions;

namespace ComputerGamesORM.Business;

public sealed class GameService : IGameService
{
    public const int MaxNameLength = 200;
    public const int MaxDescriptionLength = 2000;

    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<IReadOnlyCollection<GameDto>> GetAllAsync(string? searchText = null, CancellationToken cancellationToken = default)
    {
        var games = await _gameRepository.GetAllAsync(searchText, cancellationToken);
        return games.Select(MapToDto).ToList();
    }

    public async Task<GameDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            return null;
        }

        var game = await _gameRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        return game is null ? null : MapToDto(game);
    }

    public async Task<int> CreateAsync(GameEditModel model, CancellationToken cancellationToken = default)
    {
        var normalized = Normalize(model);

        if (await _gameRepository.ExistsByNameAsync(normalized.Name, cancellationToken: cancellationToken))
        {
            throw new InvalidOperationException($"Game with name '{normalized.Name}' already exists.");
        }

        var game = new Game
        {
            Name = normalized.Name,
            GameDescription = new GameDescription
            {
                Description = normalized.Description
            }
        };

        try
        {
            return await _gameRepository.AddAsync(game, cancellationToken);
        }
        catch (DataConflictException ex)
        {
            throw new InvalidOperationException($"Game with name '{normalized.Name}' already exists.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, GameEditModel model, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            return false;
        }

        var normalized = Normalize(model);
        var existingGame = await _gameRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (existingGame is null)
        {
            return false;
        }

        if (await _gameRepository.ExistsByNameAsync(normalized.Name, id, cancellationToken))
        {
            throw new InvalidOperationException($"Another game with name '{normalized.Name}' already exists.");
        }

        try
        {
            return await _gameRepository.UpdateAsync(id, normalized.Name, normalized.Description, cancellationToken);
        }
        catch (DataConflictException ex)
        {
            throw new InvalidOperationException($"Another game with name '{normalized.Name}' already exists.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            return false;
        }

        return await _gameRepository.DeleteAsync(id, cancellationToken);
    }

    private static (string Name, string Description) Normalize(GameEditModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        if (string.IsNullOrWhiteSpace(model.Name))
        {
            throw new ArgumentException("Game name cannot be empty.", nameof(model));
        }

        if (string.IsNullOrWhiteSpace(model.Description))
        {
            throw new ArgumentException("Game description cannot be empty.", nameof(model));
        }

        var normalizedName = model.Name.Trim();
        if (normalizedName.Length > MaxNameLength)
        {
            throw new ArgumentException($"Game name must be up to {MaxNameLength} characters.", nameof(model));
        }

        var normalizedDescription = model.Description.Trim();
        if (normalizedDescription.Length > MaxDescriptionLength)
        {
            throw new ArgumentException($"Game description must be up to {MaxDescriptionLength} characters.", nameof(model));
        }

        return (normalizedName, normalizedDescription);
    }

    private static GameDto MapToDto(Game game)
    {
        return new GameDto(
            game.Id,
            game.Name,
            game.GameDescription?.Description ?? string.Empty);
    }
}
