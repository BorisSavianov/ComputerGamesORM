using ComputerGamesORM.Business;
using ComputerGamesORM.Data;
using ComputerGamesORM.Presentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true)
    .Build();

var settings = configuration.Get<AppSettings>() ?? new AppSettings();
var asciiRenderer = new AsciiRenderer(settings.EnableAsciiUI);

var dbPath = Path.Combine(AppContext.BaseDirectory, "ComputerGamesORM.db");
var options = new DbContextOptionsBuilder<ComputerGamesContext>()
    .UseSqlite($"Data Source={dbPath}")
    .EnableDetailedErrors()
    .Options;

using var dbContext = new ComputerGamesContext(options);
await dbContext.Database.EnsureCreatedAsync();

IGameService gameService = new GameService(dbContext);
var ui = new ConsoleUi(gameService, asciiRenderer);

try
{
    await ui.RunAsync();
}
catch (Exception ex)
{
    asciiRenderer.Error($"Unexpected error: {ex.Message}");
}
