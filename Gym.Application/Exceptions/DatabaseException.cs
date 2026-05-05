namespace Gym.Application.Exceptions;

public sealed class DatabaseException : Exception
{
    public DatabaseException(string message) : base(message) { }
}