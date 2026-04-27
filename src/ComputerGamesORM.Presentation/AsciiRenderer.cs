namespace ComputerGamesORM.Presentation;

public sealed class AsciiRenderer
{
    private readonly bool _isEnabled;

    public AsciiRenderer(bool isEnabled)
    {
        _isEnabled = isEnabled;
    }

    public void RenderBanner()
    {
        if (!_isEnabled)
        {
            return;
        }

        Console.WriteLine("  ____    _    __  __ _____ ____  ");
        Console.WriteLine(" / ___|  / \\  |  \\/  | ____/ ___| ");
        Console.WriteLine("| |  _  / _ \\ | |\\/| |  _| \\___ \\ ");
        Console.WriteLine("| |_| |/ ___ \\| |  | | |___ ___) |");
        Console.WriteLine(" \\____/_/   \\_\\_|  |_|_____|____/ ");
        Console.WriteLine();
    }

    public void Success(string message)
    {
        if (_isEnabled)
        {
            Console.WriteLine($"✔ {message}");
            return;
        }

        Console.WriteLine(message);
    }

    public void Error(string message)
    {
        if (_isEnabled)
        {
            Console.WriteLine($"✖ {message}");
            return;
        }

        Console.WriteLine(message);
    }

    public void ListHint()
    {
        if (_isEnabled)
        {
            Console.WriteLine("Listing games...");
        }
    }
}
