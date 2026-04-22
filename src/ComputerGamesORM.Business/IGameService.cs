namespace ComputerGamesORM.Business;

public interface IGameService
{
    Task<IReadOnlyCollection<GameDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<GameDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<int> AddAsync(string gameName, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(int id, string gameName, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
