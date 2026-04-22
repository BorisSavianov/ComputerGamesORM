using ComputerGamesORM.Data;
using Microsoft.EntityFrameworkCore;

namespace ComputerGamesORM.Tests;

internal static class InMemoryDbContext
{
    public static ComputerGamesContext Create(string databaseName)
    {
        var options = new DbContextOptionsBuilder<ComputerGamesContext>()
            .UseInMemoryDatabase(databaseName)
            .EnableDetailedErrors()
            .Options;

        return new ComputerGamesContext(options);
    }
}
