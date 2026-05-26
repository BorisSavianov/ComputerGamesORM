using ComputerGamesORM.Business;
using ComputerGamesORM.Data.Models;
using NUnit.Framework;

namespace ComputerGamesORM.Tests;

[TestFixture]
public sealed class GameServiceTests
{
    [Test]
    public async Task CreateGame_WithValidInput_ShouldSucceed()
    {
        using var database = new SqliteTestDatabase();
        var service = database.CreateService();

        var id = await service.CreateAsync(new GameEditModel("Cyberpunk 2077", "Open-world role-playing game."));

        using var context = database.CreateContext();
        Assert.That(id, Is.GreaterThan(0));
        Assert.That(context.Games.Count(), Is.EqualTo(1));
        Assert.That(context.GameDescriptions.Count(), Is.EqualTo(1));
    }

    [Test]
    public void CreateGame_WithEmptyName_ShouldThrow()
    {
        using var database = new SqliteTestDatabase();
        var service = database.CreateService();

        Assert.ThrowsAsync<ArgumentException>(async () => await service.CreateAsync(new GameEditModel(string.Empty, "Description")));
        Assert.ThrowsAsync<ArgumentException>(async () => await service.CreateAsync(new GameEditModel("   ", "Description")));
    }

    [Test]
    public void CreateGame_WithEmptyDescription_ShouldThrow()
    {
        using var database = new SqliteTestDatabase();
        var service = database.CreateService();

        Assert.ThrowsAsync<ArgumentException>(async () => await service.CreateAsync(new GameEditModel("Portal", string.Empty)));
        Assert.ThrowsAsync<ArgumentException>(async () => await service.CreateAsync(new GameEditModel("Portal", "   ")));
    }

    [Test]
    public async Task CreateGame_WithDuplicateName_ShouldThrow()
    {
        using var database = new SqliteTestDatabase();
        var service = database.CreateService();
        await service.CreateAsync(new GameEditModel("Portal", "First description."));

        Assert.ThrowsAsync<InvalidOperationException>(async () => await service.CreateAsync(new GameEditModel("Portal", "Second description.")));
    }

    [Test]
    public async Task GetGameById_WithValidId_ShouldReturnEntityWithDescription()
    {
        using var database = new SqliteTestDatabase();
        var id = await SeedGameAsync(database, "Half-Life", "Science-fiction shooter.");
        var service = database.CreateService();

        var game = await service.GetByIdAsync(id);

        Assert.That(game, Is.Not.Null);
        Assert.That(game!.Name, Is.EqualTo("Half-Life"));
        Assert.That(game.Description, Is.EqualTo("Science-fiction shooter."));
    }

    [Test]
    public async Task GetGameById_WithInvalidId_ShouldReturnNull()
    {
        using var database = new SqliteTestDatabase();
        var service = database.CreateService();

        var game = await service.GetByIdAsync(-1);

        Assert.That(game, Is.Null);
    }

    [Test]
    public async Task UpdateGame_WithValidId_ShouldUpdate()
    {
        using var database = new SqliteTestDatabase();
        var id = await SeedGameAsync(database, "Dota", "Original description.");
        var service = database.CreateService();

        var result = await service.UpdateAsync(id, new GameEditModel("Dota 2", "Updated multiplayer strategy game."));

        using var context = database.CreateContext();
        Assert.That(result, Is.True);
        Assert.That(context.Games.Single().Name, Is.EqualTo("Dota 2"));
        Assert.That(context.GameDescriptions.Single().Description, Is.EqualTo("Updated multiplayer strategy game."));
    }

    [Test]
    public async Task UpdateGame_WithInvalidId_ShouldReturnFalse()
    {
        using var database = new SqliteTestDatabase();
        var service = database.CreateService();

        var result = await service.UpdateAsync(999, new GameEditModel("No Game", "Description."));

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task DeleteGame_WithValidId_ShouldRemoveEntityAndDescription()
    {
        using var database = new SqliteTestDatabase();
        var id = await SeedGameAsync(database, "Hades", "Roguelike action game.");
        var service = database.CreateService();

        var result = await service.DeleteAsync(id);

        using var context = database.CreateContext();
        Assert.That(result, Is.True);
        Assert.That(context.Games.Any(), Is.False);
        Assert.That(context.GameDescriptions.Any(), Is.False);
    }

    [Test]
    public async Task DeleteGame_WithInvalidId_ShouldNotCrash()
    {
        using var database = new SqliteTestDatabase();
        var service = database.CreateService();

        var result = await service.DeleteAsync(1000);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetAll_ShouldReturnCorrectList()
    {
        using var database = new SqliteTestDatabase();
        await SeedGameAsync(database, "Portal", "Puzzle game.");
        await SeedGameAsync(database, "Doom", "Shooter.");
        var service = database.CreateService();

        var games = await service.GetAllAsync();

        Assert.That(games.Count, Is.EqualTo(2));
        Assert.That(games.Select(g => g.Name), Is.EquivalentTo(new[] { "Portal", "Doom" }));
        Assert.That(games.Select(g => g.Description), Is.EquivalentTo(new[] { "Puzzle game.", "Shooter." }));
    }

    [Test]
    public async Task GetAll_WithSearchText_ShouldFilterByNameDescriptionOrId()
    {
        using var database = new SqliteTestDatabase();
        var portalId = await SeedGameAsync(database, "Portal", "Puzzle game.");
        await SeedGameAsync(database, "Doom", "Shooter.");
        var service = database.CreateService();

        var byName = await service.GetAllAsync("porta");
        var byDescription = await service.GetAllAsync("shoot");
        var byId = await service.GetAllAsync(portalId.ToString());

        Assert.That(byName.Select(g => g.Name), Is.EqualTo(new[] { "Portal" }));
        Assert.That(byDescription.Select(g => g.Name), Is.EqualTo(new[] { "Doom" }));
        Assert.That(byId.Select(g => g.Name), Is.EqualTo(new[] { "Portal" }));
    }

    private static async Task<int> SeedGameAsync(SqliteTestDatabase database, string name, string description)
    {
        using var context = database.CreateContext();
        var game = new Game
        {
            Name = name,
            GameDescription = new GameDescription
            {
                Description = description
            }
        };

        context.Games.Add(game);
        await context.SaveChangesAsync();
        return game.Id;
    }
}
