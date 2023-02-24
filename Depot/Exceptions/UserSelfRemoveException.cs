namespace Depot.Exceptions;

public class UserSelfRemoveException : Exception
{
    public UserSelfRemoveException() : base()
    {
    }

    public UserSelfRemoveException(string message) : base(message)
    {
    }

    public UserSelfRemoveException(string message, Exception innerException) : base(message, innerException)
    {
    }
}