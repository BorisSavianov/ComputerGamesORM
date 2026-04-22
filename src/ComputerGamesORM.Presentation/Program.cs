using ComputerGamesORM.Business;
using ComputerGamesORM.Data;
using ComputerGamesORM.Presentation;
using Microsoft.EntityFrameworkCore;

var dbPath = Path.Combine(AppContext.BaseDirectory, "ComputerGamesORM.db");
var options = new DbContextOptionsBuilder<ComputerGamesContext>()
    .UseSqlite($"Data Source={dbPath}")
    .EnableDetailedErrors()
    .Options;

using var dbContext = new ComputerGamesContext(options);
await dbContext.Database.EnsureCreatedAsync();

IGameService gameService = new GameService(dbContext);
var ui = new ConsoleUi(gameService);

try
{
    await ui.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}
