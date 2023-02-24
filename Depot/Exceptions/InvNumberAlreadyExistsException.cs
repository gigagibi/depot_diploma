namespace Depot.Exceptions;

public class InvNumberAlreadyExistsException : Exception
{
    public InvNumberAlreadyExistsException() : base()
    {
    }

    public InvNumberAlreadyExistsException(string message) : base(message)
    {
    }

    public InvNumberAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}