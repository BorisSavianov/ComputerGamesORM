using ComputerGamesORM.Business;
using ComputerGamesORM.Data.Models;

namespace ComputerGamesORM.Tests;

[TestFixture]
public sealed class GameServiceTests
{
    [Test]
    public async Task AddGame_WithValidInput_ShouldSucceed()
    {
        // Arrange
        using var context = InMemoryDbContext.Create(nameof(AddGame_WithValidInput_ShouldSucceed));
        var service = new GameService(context);

        // Act
        var id = await service.AddAsync("Cyberpunk 2077");

        // Assert
        Assert.That(id, Is.GreaterThan(0));
        Assert.That(context.Games.Count(), Is.EqualTo(1));
    }

    [Test]
    public void AddGame_WithEmptyOrNull_ShouldThrow()
    {
        // Arrange
        using var context = InMemoryDbContext.Create(nameof(AddGame_WithEmptyOrNull_ShouldThrow));
        var service = new GameService(context);

        // Act + Assert
        Assert.ThrowsAsync<ArgumentException>(async () => await service.AddAsync(string.Empty));
        Assert.ThrowsAsync<ArgumentException>(async () => await service.AddAsync("   "));
        Assert.ThrowsAsync<ArgumentException>(async () => await service.AddAsync(null!));
    }

    [Test]
    public async Task GetGameById_WithValidId_ShouldReturnEntity()
    {
        // Arrange
        using var context = InMemoryDbContext.Create(nameof(GetGameById_WithValidId_ShouldReturnEntity));
        context.Games.Add(new Game { Name = "Half-Life" });
        await context.SaveChangesAsync();

        var service = new GameService(context);
        var id = context.Games.Single().Id;

        // Act
        var game = await service.GetByIdAsync(id);

        // Assert
        Assert.That(game, Is.Not.Null);
        Assert.That(game!.Name, Is.EqualTo("Half-Life"));
    }

    [Test]
    public async Task GetGameById_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        using var context = InMemoryDbContext.Create(nameof(GetGameById_WithInvalidId_ShouldReturnNull));
        var service = new GameService(context);

        // Act
        var game = await service.GetByIdAsync(-1);

        // Assert
        Assert.That(game, Is.Null);
    }

    [Test]
    public async Task UpdateGame_WithValidId_ShouldUpdate()
    {
        // Arrange
        using var context = InMemoryDbContext.Create(nameof(UpdateGame_WithValidId_ShouldUpdate));
        context.Games.Add(new Game { Name = "Dota" });
        await context.SaveChangesAsync();

        var service = new GameService(context);
        var id = context.Games.Single().Id;

        // Act
        var result = await service.UpdateAsync(id, "Dota 2");

        // Assert
        Assert.That(result, Is.True);
        Assert.That(context.Games.Single().Name, Is.EqualTo("Dota 2"));
    }

    [Test]
    public async Task UpdateGame_WithInvalidId_ShouldReturnFalse()
    {
        // Arrange
        using var context = InMemoryDbContext.Create(nameof(UpdateGame_WithInvalidId_ShouldReturnFalse));
        var service = new GameService(context);

        // Act
        var result = await service.UpdateAsync(999, "No Game");

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task DeleteGame_WithValidId_ShouldRemoveEntity()
    {
        // Arrange
        using var context = InMemoryDbContext.Create(nameof(DeleteGame_WithValidId_ShouldRemoveEntity));
        context.Games.Add(new Game { Name = "Hades" });
        await context.SaveChangesAsync();

        var service = new GameService(context);
        var id = context.Games.Single().Id;

        // Act
        var result = await service.DeleteAsync(id);

        // Assert
        Assert.That(result, Is.True);
        Assert.That(context.Games.Any(), Is.False);
    }

    [Test]
    public async Task DeleteGame_WithInvalidId_ShouldNotCrash()
    {
        // Arrange
        using var context = InMemoryDbContext.Create(nameof(DeleteGame_WithInvalidId_ShouldNotCrash));
        var service = new GameService(context);

        // Act
        var result = await service.DeleteAsync(1000);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetAll_ShouldReturnCorrectList()
    {
        // Arrange
        using var context = InMemoryDbContext.Create(nameof(GetAll_ShouldReturnCorrectList));
        context.Games.AddRange(
            new Game { Name = "Portal" },
            new Game { Name = "Doom" });
        await context.SaveChangesAsync();

        var service = new GameService(context);

        // Act
        var games = await service.GetAllAsync();

        // Assert
        Assert.That(games.Count, Is.EqualTo(2));
        Assert.That(games.Select(g => g.Name), Is.EquivalentTo(new[] { "Portal", "Doom" }));
    }
}
