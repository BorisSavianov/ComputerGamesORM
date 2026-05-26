using ComputerGamesORM.Business;
using ComputerGamesORM.Data;
using ComputerGamesORM.Data.Repositories;
using ComputerGamesORM.Presentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true)
    .Build();

var settings = configuration.Get<AppSettings>() ?? new AppSettings();
var asciiRenderer = new AsciiRenderer(settings.EnableAsciiUI);

var connectionString = configuration.GetConnectionString("DefaultConnection");
var options = new DbContextOptionsBuilder<ComputerGamesContext>()
    .UseSqlServer(connectionString)
    .EnableDetailedErrors()
    .Options;

var dbContextFactory = new ConsoleDbContextFactory(options);
var databaseInitializer = new DatabaseInitializer(dbContextFactory);

// Simple retry logic to wait for SQL Server to be ready
var retryCount = 0;
const int maxRetries = 10;
while (retryCount < maxRetries)
{
    try
    {
        await databaseInitializer.InitializeAsync();
        break;
    }
    catch (Exception)
    {
        retryCount++;
        if (retryCount == maxRetries)
        {
            asciiRenderer.Error("Could not connect to the database. Please make sure SQL Server is running.");
            return;
        }
        Console.WriteLine($"Database not ready, retrying ({retryCount}/{maxRetries})...");
        await Task.Delay(2000);
    }
}

IGameService gameService = new GameService(new GameRepository(dbContextFactory));
var ui = new ConsoleUi(gameService, asciiRenderer);

try
{
    await ui.RunAsync();
}
catch (Exception ex)
{
    asciiRenderer.Error($"Unexpected error: {ex.Message}");
}

internal sealed class ConsoleDbContextFactory : IDbContextFactory<ComputerGamesContext>
{
    private readonly DbContextOptions<ComputerGamesContext> _options;

    public ConsoleDbContextFactory(DbContextOptions<ComputerGamesContext> options)
    {
        _options = options;
    }

    public ComputerGamesContext CreateDbContext()
    {
        return new ComputerGamesContext(_options);
    }
}
