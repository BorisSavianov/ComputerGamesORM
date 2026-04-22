using ComputerGamesORM.Business;

namespace ComputerGamesORM.Presentation;

public sealed class ConsoleUi
{
    private readonly IGameService _gameService;

    public ConsoleUi(IGameService gameService)
    {
        _gameService = gameService;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            PrintMenu();
            Console.Write("Choose option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await ListAllGamesAsync(cancellationToken);
                    break;
                case "2":
                    await AddGameAsync(cancellationToken);
                    break;
                case "3":
                    await UpdateGameAsync(cancellationToken);
                    break;
                case "4":
                    await FetchByIdAsync(cancellationToken);
                    break;
                case "5":
                    await DeleteByIdAsync(cancellationToken);
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("--------------- MENU ------------------");
        Console.WriteLine("--------------------------------------");
        Console.WriteLine("1. List all computer games");
        Console.WriteLine("2. Add new computer game");
        Console.WriteLine("3. Update computer game");
        Console.WriteLine("4. Fetch computer game by ID");
        Console.WriteLine("5. Delete computer game by ID");
        Console.WriteLine("6. Exit");
    }

    private async Task ListAllGamesAsync(CancellationToken cancellationToken)
    {
        var games = await _gameService.GetAllAsync(cancellationToken);
        foreach (var game in games)
        {
            Console.WriteLine($"{game.Id}. {game.Name}");
        }
    }

    private async Task AddGameAsync(CancellationToken cancellationToken)
    {
        Console.Write("Game name: ");
        var gameName = Console.ReadLine() ?? string.Empty;

        try
        {
            await _gameService.AddAsync(gameName, cancellationToken);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async Task UpdateGameAsync(CancellationToken cancellationToken)
    {
        var id = ReadPositiveInteger("Game ID: ");
        Console.Write("New game name: ");
        var gameName = Console.ReadLine() ?? string.Empty;

        try
        {
            var isUpdated = await _gameService.UpdateAsync(id, gameName, cancellationToken);
            if (!isUpdated)
            {
                Console.WriteLine("Game not found!");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async Task FetchByIdAsync(CancellationToken cancellationToken)
    {
        var id = ReadPositiveInteger("Game ID: ");
        var game = await _gameService.GetByIdAsync(id, cancellationToken);

        if (game is not null)
        {
            Console.WriteLine(game.Name);
        }
    }

    private async Task DeleteByIdAsync(CancellationToken cancellationToken)
    {
        var id = ReadPositiveInteger("Game ID: ");
        var isDeleted = await _gameService.DeleteAsync(id, cancellationToken);

        if (isDeleted)
        {
            Console.WriteLine("Done.");
        }
    }

    private static int ReadPositiveInteger(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (int.TryParse(input, out var id) && id > 0)
            {
                return id;
            }

            Console.WriteLine("Invalid numeric ID.");
        }
    }
}
