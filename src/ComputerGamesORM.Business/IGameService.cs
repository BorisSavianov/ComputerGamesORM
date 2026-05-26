namespace ComputerGamesORM.Business;

public interface IGameService
{
    Task<IReadOnlyCollection<GameDto>> GetAllAsync(string? searchText = null, CancellationToken cancellationToken = default);

    Task<GameDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<int> CreateAsync(GameEditModel model, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(int id, GameEditModel model, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
