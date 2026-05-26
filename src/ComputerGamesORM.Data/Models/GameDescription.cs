namespace ComputerGamesORM.Data.Models;

public class GameDescription
{
    public int Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public int GameId { get; set; }

    public Game Game { get; set; } = null!;
}
