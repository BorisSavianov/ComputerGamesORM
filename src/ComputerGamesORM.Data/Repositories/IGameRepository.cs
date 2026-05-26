using ComputerGamesORM.Data.Models;

namespace ComputerGamesORM.Data.Repositories;

public interface IGameRepository
{
    Task<IReadOnlyCollection<Game>> GetAllAsync(string? searchText = null, CancellationToken cancellationToken = default);

    Task<Game?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> ExistsByNameAsync(string name, int? excludedGameId = null, CancellationToken cancellationToken = default);

    Task<int> AddAsync(Game game, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(int id, string name, string description, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
