using ComputerGamesORM.Business;
using ComputerGamesORM.Data;
using ComputerGamesORM.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ComputerGamesORM.Tests;

internal sealed class SqliteTestDatabase : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<ComputerGamesContext> _options;

    public SqliteTestDatabase()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<ComputerGamesContext>()
            .UseSqlite(_connection)
            .EnableDetailedErrors()
            .Options;

        using var context = CreateContext();
        context.Database.EnsureCreated();
    }

    public ComputerGamesContext CreateContext()
    {
        return new ComputerGamesContext(_options);
    }

    public GameService CreateService()
    {
        return new GameService(new GameRepository(new TestDbContextFactory(_options)));
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    private sealed class TestDbContextFactory : IDbContextFactory<ComputerGamesContext>
    {
        private readonly DbContextOptions<ComputerGamesContext> _options;

        public TestDbContextFactory(DbContextOptions<ComputerGamesContext> options)
        {
            _options = options;
        }

        public ComputerGamesContext CreateDbContext()
        {
            return new ComputerGamesContext(_options);
        }
    }
}
