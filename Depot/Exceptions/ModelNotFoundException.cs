namespace Depot.Exceptions;

public class ModelNotFoundException : Exception
{
    public ModelNotFoundException() : base()
    {
    }

    public ModelNotFoundException(string message) : base(message)
    {
    }

    public ModelNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}