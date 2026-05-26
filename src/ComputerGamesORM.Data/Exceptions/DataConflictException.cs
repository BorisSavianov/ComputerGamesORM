namespace ComputerGamesORM.Data.Exceptions;

public sealed class DataConflictException : Exception
{
    public DataConflictException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
