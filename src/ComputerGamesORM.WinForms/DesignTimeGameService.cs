using ComputerGamesORM.Business;

namespace ComputerGamesORM.WinForms;

internal sealed class DesignTimeGameService : IGameService
{
    private static readonly IReadOnlyCollection<GameDto> Games =
    [
        new GameDto(1, "Half-Life 2", "A story-driven first-person shooter focused on physics, exploration, and resistance."),
        new GameDto(2, "Portal 2", "A puzzle game built around portal mechanics and test chambers."),
        new GameDto(3, "Hades", "A fast roguelike action game with repeated escape attempts.")
    ];

    public Task<IReadOnlyCollection<GameDto>> GetAllAsync(string? searchText = null, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Games);
    }

    public Task<GameDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Games.FirstOrDefault(game => game.Id == id));
    }

    public Task<int> CreateAsync(GameEditModel model, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(4);
    }

    public Task<bool> UpdateAsync(int id, GameEditModel model, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }
}
