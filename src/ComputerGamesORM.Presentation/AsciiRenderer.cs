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

        Console.WriteLine("  ____                        _");
        Console.WriteLine(" / ___| __ _ _ __ ___   ___ | |");
        Console.WriteLine("| |  _ / _` | '_ ` _ \\ / _ \\| |");
        Console.WriteLine("| |_| | (_| | | | | | | (_) | |");
        Console.WriteLine(" \\____|\\__,_|_| |_| |_|\\___/|_|");
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
            Console.WriteLine("📦 Listing games...");
        }
    }
}
